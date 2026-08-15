using System.Net;
using System.Text.Json;

namespace ProductManagement.API.Middleware;

public class GlobalExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<GlobalExceptionMiddleware> _logger;

    public GlobalExceptionMiddleware(
        RequestDelegate next,
        ILogger<GlobalExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unhandled exception occurred.");

            await HandleExceptionAsync(context, ex);
        }
    }

    private static async Task HandleExceptionAsync(
        HttpContext context,
        Exception exception)
    {
        if (context.Response.HasStarted)
            return;

        context.Response.StatusCode = GetStatusCode(exception);
        context.Response.ContentType = "application/json";

        var response = new
        {
            statusCode = context.Response.StatusCode,
            message = GetMessage(exception)
        };

        await context.Response.WriteAsync(
            JsonSerializer.Serialize(response));
    }

    private static int GetStatusCode(Exception exception)
    {
        return exception switch
        {
            KeyNotFoundException => (int)HttpStatusCode.NotFound,

            UnauthorizedAccessException => (int)HttpStatusCode.Unauthorized,

            ArgumentException => (int)HttpStatusCode.BadRequest,

            FormatException => (int)HttpStatusCode.BadRequest,

            InvalidOperationException => (int)HttpStatusCode.BadRequest,

            InvalidDataException => (int)HttpStatusCode.Conflict,

            _ => (int)HttpStatusCode.InternalServerError
        };
    }

    private static string GetMessage(Exception exception)
    {
        return exception switch
        {
            UnauthorizedAccessException =>
                string.IsNullOrWhiteSpace(exception.Message)
                    ? "Unauthorized."
                    : exception.Message,

            _ when !string.IsNullOrWhiteSpace(exception.Message) =>
                exception.Message,

            _ =>
                "An unexpected error occurred."
        };
    }
}