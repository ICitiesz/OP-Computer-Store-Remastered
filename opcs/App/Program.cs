using System.Text;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using opcs.App.Common;
using opcs.App.Common.DI;
using opcs.App.Common.Exception;
using opcs.App.Common.Init;
using opcs.App.Data.Validation;
using opcs.App.Database;

namespace opcs.App;

public static class Program
{
    public static void Main(string[] args)
    {
        var webAppBuilder = WebApplication.CreateBuilder(args);

        _ConfigureService(webAppBuilder);
        _BuildWebApp(webAppBuilder).Run();
    }

    private static void _ConfigureService(WebApplicationBuilder webAppBuilder)
    {
        webAppBuilder.Services.AddExceptionHandler<OpcsExceptionHandler>();
        webAppBuilder.Services.AddProblemDetails();

        webAppBuilder.Services.AddDbContextPool<AppDbContext>(dbContextBuilder =>
        {
            dbContextBuilder.UseMySql(
                webAppBuilder.Configuration.GetConnectionString("OPCSDbConnection"),
                new MySqlServerVersion("9.0.1")
            );
        });

        _ConfigureDependencyInjection(webAppBuilder);
        _ConfigureController(webAppBuilder);
        _ConfigureAuthentication(webAppBuilder);

        webAppBuilder.Services.AddEndpointsApiExplorer();
        webAppBuilder.Services.AddSwaggerGen();
        webAppBuilder.Services.AddHostedService(serviceProvider =>
            new ServiceTaskInitiator(serviceProvider, webAppBuilder.Services));
    }

    private static void _ConfigureDependencyInjection(WebApplicationBuilder webAppBuilder)
    {
        var autoFacServiceProviderFac = new AutofacServiceProviderFactory(containerBuilder =>
        {
            containerBuilder.RegisterModule<GeneralModule>();
            containerBuilder.RegisterModule<RepositoryModule>();
            containerBuilder.RegisterModule<ServiceModule>();
            containerBuilder.RegisterModule<ControllerModule>();
        });

        webAppBuilder.Host.UseServiceProviderFactory(autoFacServiceProviderFac);
    }

    private static void _ConfigureResolver(IServiceCollection serviceCollection, IServiceProvider serviceProvider)
    {
        foreach (var service in serviceCollection)
        {
            var autofacContainer = serviceProvider.GetAutofacRoot();

            if (!autofacContainer.IsRegistered(service.GetType())) continue;

            serviceProvider.GetAutofacRoot().Resolve<IServiceProvider>();
        }
    }

    private static void _ConfigureAuthentication(WebApplicationBuilder webAppBuilder)
    {
        webAppBuilder.Services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(option =>
            {
                option.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = webAppBuilder.Configuration["Jwt:Issuer"],
                    ValidAudience = webAppBuilder.Configuration["Jwt:Audience"],
                    IssuerSigningKey =
                        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(webAppBuilder.Configuration["Jwt:SigningKey"]!)),
                    ClockSkew = TimeSpan.FromSeconds(5)
                };
            });
    }

    private static void _ConfigureController(WebApplicationBuilder webAppBuilder)
    {
        webAppBuilder.Services.AddMvc().AddControllersAsServices()
            .AddMvcOptions(options =>
            {
                options.Filters.Add<EndpointValidator>();
            })
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.PropertyNamingPolicy = null;
            });
    }

    private static void _ConfigureSwagger(WebApplication webApp, AppConfiguration appConfig)
    {
        webApp.UseSwagger(swaggerOption =>
        {
            swaggerOption.RouteTemplate = "swagger/{documentName}/swagger.json";
            swaggerOption.PreSerializeFilters.Add((swaggerDoc, httpReq) =>
            {
                swaggerDoc.Servers = new List<OpenApiServer>
                    { new() { Url = $"{httpReq.Scheme}://{httpReq.Host.Value}{appConfig.GetBasePath()}" } };
            });
        });
        webApp.UseSwaggerUI();
    }

    private static WebApplication _BuildWebApp(WebApplicationBuilder webAppBuilder)
    {
        var webApp = webAppBuilder.Build();
        var appConfig = new AppConfiguration(webAppBuilder.Configuration);

        webApp.UseExceptionHandler(new ExceptionHandlerOptions());

        _ConfigureResolver(webAppBuilder.Services, webApp.Services);

        if (webApp.Environment.IsDevelopment())
        {
           _ConfigureSwagger(webApp, appConfig);
        }

        webApp.UsePathBase(appConfig.GetBasePath());
        webApp.UseRouting();
        webApp.UseHttpsRedirection();
        webApp.UseAuthentication();
        webApp.UseAuthorization();
        webApp.MapControllers();

        return webApp;
    }


}