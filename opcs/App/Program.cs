using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using opcs.App.Core;
using opcs.App.Core.DI;
using opcs.App.Core.Exception;
using opcs.App.Core.Init;
using opcs.App.Core.Security;
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
        var appConfig = new AppConfiguration(webAppBuilder.Configuration);

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
        _ConfigureAuthentication(webAppBuilder, appConfig);

        webAppBuilder.Services.AddEndpointsApiExplorer();
        webAppBuilder.Services.AddSwaggerGen();
        webAppBuilder.Services.AddHostedService(serviceProvider =>
            new ServiceTaskInitiator(serviceProvider, webAppBuilder.Services));

        webAppBuilder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowSpecificOrigin",
                builder => builder
                    .SetIsOriginAllowed(_ => true)
                    .WithOrigins("http://localhost:4200")
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials());
        });
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

    private static void _ConfigureAuthentication(WebApplicationBuilder webAppBuilder, AppConfiguration appConfig)
    {
        webAppBuilder.Services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddCookie(option => { option.Cookie.Name = "accessToken"; })
            .AddJwtBearer(option =>
            {
                option.Events = new JwtAuthEvents();
                option.TokenValidationParameters = appConfig.GetTokenValidationParameters();
            });
    }

    private static void _ConfigureController(WebApplicationBuilder webAppBuilder)
    {
        webAppBuilder.Services.AddMvc().AddControllersAsServices()
            .AddMvcOptions(options => { options.Filters.Add<EndpointValidator>(); })
            .AddJsonOptions(options => { options.JsonSerializerOptions.PropertyNamingPolicy = null; });
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

        if (webApp.Environment.IsDevelopment()) _ConfigureSwagger(webApp, appConfig);

        webApp.UsePathBase(appConfig.GetBasePath());
        webApp.UseCors("AllowSpecificOrigin");
        webApp.UseAuthentication();
        webApp.UseAuthorization();
        webApp.UseRouting();
        webApp.UseHttpsRedirection();
        webApp.MapControllers();

        return webApp;
    }
}