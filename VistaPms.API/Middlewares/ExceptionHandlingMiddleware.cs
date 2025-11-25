using Microsoft.AspNetCore.Diagnostics;
using System.Net;
using System.Text.Json;
using VistaPms.Application.Common.Exceptions;

using Sentry;

namespace VistaPms.API.Middlewares;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
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
            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        SentrySdk.CaptureException(exception);
        _logger.LogError(exception, "An error occurred: {Message}", exception.Message);

        var statusCode = exception switch
        {
            Application.Common.Exceptions.ValidationException => (int)HttpStatusCode.BadRequest,
            NotFoundException => (int)HttpStatusCode.NotFound,
            _ => (int)HttpStatusCode.InternalServerError
        };

        var problemDetails = new Microsoft.AspNetCore.Mvc.ProblemDetails
        {
            Status = statusCode,
            Title = exception switch
            {
                Application.Common.Exceptions.ValidationException => "Validation Failed",
                NotFoundException => "Resource Not Found",
                _ => "An internal error occurred"
            },
            Detail = exception.Message,
            Instance = context.Request.Path
        };

        if (exception is Application.Common.Exceptions.ValidationException validationException)
        {
            problemDetails.Extensions["errors"] = validationException.Errors;
        }

        context.Response.StatusCode = statusCode;
        context.Response.ContentType = "application/problem+json";
        
        await context.Response.WriteAsync(JsonSerializer.Serialize(problemDetails));
    }
}
