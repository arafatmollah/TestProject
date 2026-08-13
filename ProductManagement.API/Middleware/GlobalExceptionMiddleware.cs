using FluentValidation;
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
            _logger.LogError(
                ex,
                "An unhandled exception occurred.");

            await HandleExceptionAsync(context, ex);
        }
    }

    private static async Task HandleExceptionAsync(
        HttpContext context,
        Exception exception)
    {
        if (context.Response.HasStarted)
        {
            return;
        }

        context.Response.Clear();
        context.Response.ContentType = "application/json";

        var (statusCode, message, errors) = GetExceptionDetails(exception);

        context.Response.StatusCode = statusCode;

        var response = new
        {
            statusCode,
            message,
            errors
        };

        await context.Response.WriteAsync(
            JsonSerializer.Serialize(response));
    }

    private static (
        int StatusCode,
        string Message,
        object? Errors
    ) GetExceptionDetails(Exception exception)
    {
        return exception switch
        {
            ValidationException validationException =>
                (
                    (int)HttpStatusCode.BadRequest,
                    "Validation failed.",
                    validationException.Errors
                        .GroupBy(x => x.PropertyName)
                        .ToDictionary(
                            x => x.Key,
                            x => x.Select(e => e.ErrorMessage).ToArray())
                ),

       
            KeyNotFoundException =>
                (
                    (int)HttpStatusCode.NotFound,
                    exception.Message,
                    null
                ),

   
            UnauthorizedAccessException =>
                (
                    (int)HttpStatusCode.Unauthorized,
                    string.IsNullOrWhiteSpace(exception.Message)
                        ? "Unauthorized."
                        : exception.Message,
                    null
                ),

 
            ArgumentException =>
                (
                    (int)HttpStatusCode.BadRequest,
                    exception.Message,
                    null
                ),

       
            FormatException =>
                (
                    (int)HttpStatusCode.BadRequest,
                    exception.Message,
                    null
                ),

      
            InvalidOperationException =>
                (
                    (int)HttpStatusCode.BadRequest,
                    exception.Message,
                    null
                ),

         
            InvalidDataException =>
                (
                    (int)HttpStatusCode.Conflict,
                    exception.Message,
                    null
                ),

        
            _ =>
                (
                    (int)HttpStatusCode.InternalServerError,
                    "An unexpected error occurred.",
                    null
                )
        };
    }
}