using System.Text.Json;
using Microsoft.AspNetCore.Mvc.Filters;
using opcs.App.Common;
using AppContext = opcs.App.Common.AppContext;

namespace opcs.App.Data.Validation;

public class EndpointValidator(AppConfiguration appConfig) : ActionFilterAttribute
{
    public override void OnResultExecuting(ResultExecutingContext context)
    {
        /* Validate if the endpoint contains base path */
        var reqPathBase = context.HttpContext.Request.PathBase;

        if (!reqPathBase.HasValue || !reqPathBase.Equals(appConfig.GetBasePath()))
        {
            context.Result = new Response(
                statusCode: StatusCodes.Status404NotFound,
                message: AppContext.GetCodeMessage("opcs.error.request.invalid_endpoint")
            );
            return;
        }

        if (context.ModelState.IsValid) return;

        /* Collect and map field errors if available */
        var error = context.ModelState
            .Where(state => state.Value is { Errors.Count: > 0 })
            .ToDictionary(
                state => JsonNamingPolicy.CamelCase.ConvertName(state.Key),
                state => state.Value!.Errors.Select(e => e.ErrorMessage).ToList()
            );

        context.Result = new Response(
            dto: error,
            statusCode: StatusCodes.Status400BadRequest,
            message: AppContext.GetCodeMessage("opcs.error.request.invalid_property")
        );
    }
}