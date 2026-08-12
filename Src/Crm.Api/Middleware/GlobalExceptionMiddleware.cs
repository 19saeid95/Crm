using Microsoft.AspNetCore.Mvc;
using System.Net;
using FluentValidation;

namespace Crm.Api.Middleware;

public sealed class GlobalExceptionMiddleware(
    RequestDelegate next,
    ILogger<GlobalExceptionMiddleware> logger)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception exception)
        {
            await HandleExceptionAsync(
                context,
                exception);
        }
    }

    private static async Task HandleValidationExceptionAsync(
    HttpContext context,
    ValidationException exception)
    {
        var errors = exception.Errors
            .GroupBy(x => x.PropertyName)
            .ToDictionary(
                group => group.Key,
                group => group
                    .Select(x => x.ErrorMessage)
                    .ToArray());

        var problemDetails = new ValidationProblemDetails(errors)
        {
            Status = StatusCodes.Status400BadRequest,
            Title = "خطای اعتبارسنجی",
            Instance = context.Request.Path
        };

        problemDetails.Extensions["traceId"] =
            context.TraceIdentifier;

        context.Response.StatusCode =
            StatusCodes.Status400BadRequest;

        context.Response.ContentType =
            "application/problem+json";

        await context.Response.WriteAsJsonAsync(problemDetails);
    }

    private async Task HandleExceptionAsync(
     HttpContext context,
     Exception exception)
    {
        logger.LogError(
            exception,
            "Unhandled exception occurred. TraceId: {TraceId}",
            context.TraceIdentifier);

        if (exception is ValidationException validationException)
        {
            await HandleValidationExceptionAsync(
                context,
                validationException);

            return;
        }

        var statusCode = exception switch
        {
            UnauthorizedAccessException =>
                StatusCodes.Status401Unauthorized,

            InvalidOperationException =>
                StatusCodes.Status409Conflict,

            _ =>
                StatusCodes.Status500InternalServerError
        };

        var detail = context.RequestServices
            .GetRequiredService<IHostEnvironment>()
            .IsDevelopment()
                ? exception.Message
                : "خطایی در پردازش درخواست رخ داده است.";

        var problemDetails = new ProblemDetails
        {
            Status = statusCode,
            Title = GetTitle(statusCode),
            Detail = detail,
            Instance = context.Request.Path
        };

        problemDetails.Extensions["traceId"] =
            context.TraceIdentifier;

        context.Response.StatusCode = statusCode;
        context.Response.ContentType =
            "application/problem+json";

        await context.Response.WriteAsJsonAsync(problemDetails);
    }

    private static string GetTitle(int statusCode)
    {
        return statusCode switch
        {
            StatusCodes.Status401Unauthorized =>
                "احراز هویت ناموفق",

            StatusCodes.Status409Conflict =>
                "تعارض در درخواست",

            StatusCodes.Status500InternalServerError =>
                "خطای داخلی سرور",

            _ =>
                "خطا"
        };
    }
}