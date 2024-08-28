using Microsoft.AspNetCore.Diagnostics;
using opcs.App.Data;

namespace opcs.App.Common.Exception;

public class OpcsExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, System.Exception exception,
        CancellationToken cancellationToken)
    {
        var responseStatusCode = exception is OpcsException opcsException
            ? opcsException.ResponseStatusCode
            : StatusCodes.Status500InternalServerError;
        var response = new Response
        {
            statusCode = responseStatusCode,
            message = exception.Message
        };

        httpContext.Response.StatusCode = responseStatusCode;
        await httpContext.Response.WriteAsJsonAsync(response.ToJson(), cancellationToken);

        return true;
    }
}