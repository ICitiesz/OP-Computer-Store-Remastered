using System.Text.Json;
using Microsoft.AspNetCore.Mvc;

namespace opcs.App.Data;

public class Response : ActionResult
{
    private static readonly JsonSerializerOptions JsonSerializerOptions =
        new() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

    public object? dto { get; init; } = null;
    public string contentType { get; init; } = "application/json";
    public int statusCode { get; init; } = StatusCodes.Status200OK;
    public string? message { get; init; }

    public override async Task ExecuteResultAsync(ActionContext context)
    {
        var actionContextResponse = context.HttpContext.Response;

        actionContextResponse.StatusCode = statusCode;

        await actionContextResponse.WriteAsJsonAsync(ToJson());
    }

    public JsonElement? ToJson()
    {
        return JsonSerializer.SerializeToElement
        (new
        {
            contentType,
            statusCode,
            message,
            result = dto
        }, JsonSerializerOptions);
    }
}