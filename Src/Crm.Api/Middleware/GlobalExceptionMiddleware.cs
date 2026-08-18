using Crm.Application.Common.Exceptions;
using FluentValidation;

namespace Crm.Api.Middleware;

public sealed class GlobalExceptionMiddleware(RequestDelegate next, ILogger<GlobalExceptionMiddleware> logger)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "Unhandled exception occurred.");
            await HandleExceptionAsync(context, exception);
        }
    }

    private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        switch (exception)
        {
            case ValidationException validationException:

                context.Response.StatusCode = StatusCodes.Status400BadRequest;

                var errors = validationException.Errors.GroupBy(x => x.PropertyName).ToDictionary(
                        group => group.Key,
                        group => group
                            .Select(x => x.ErrorMessage)
                            .ToArray());

                await context.Response.WriteAsJsonAsync(
                    new
                    {
                        success = false,
                        statusCode = 400,
                        message = "اطلاعات وارد شده معتبر نیست.",
                        errors
                    });

                return;

            case UnauthorizedException:

                context.Response.StatusCode =StatusCodes.Status401Unauthorized;

                await context.Response.WriteAsJsonAsync(
                    new
                    {
                        success = false,
                        statusCode = 401,
                        message = exception.Message
                    });

                return;

            case ForbiddenException:

                context.Response.StatusCode =StatusCodes.Status403Forbidden;

                await context.Response.WriteAsJsonAsync(
                    new
                    {
                        success = false,
                        statusCode = 403,
                        message = exception.Message
                    });

                return;

            default:

                context.Response.StatusCode = StatusCodes.Status500InternalServerError;

                await context.Response.WriteAsJsonAsync(
                    new
                    {
                        success = false,
                        statusCode = 500,
                        message = "خطای داخلی سرور رخ داده است."
                    });

                return;
        }
    }
}