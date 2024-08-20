using System.Text.Json;
using Microsoft.AspNetCore.Mvc;

namespace opcs.App.Data;

public class Response(
    object? dto = null,
    string contentType = "application/json",
    int statusCode = StatusCodes.Status200OK,
    string? message = null) : ActionResult
{
    private static readonly JsonSerializerOptions JsonSerializerOptions =
        new() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

    public override Task ExecuteResultAsync(ActionContext context)
    {
        var actionContextResponse = context.HttpContext.Response;

        actionContextResponse.StatusCode = statusCode;

        return actionContextResponse.WriteAsJsonAsync(ToJson());
    }

    public JsonElement? ToJson()
    {
        return JsonSerializer.SerializeToElement(new { contentType, statusCode, message, result = dto },
            JsonSerializerOptions);
    }
}