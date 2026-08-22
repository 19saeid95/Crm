using Crm.Api.Models;
using Crm.Application.Common.Exceptions;
using FluentValidation;

namespace Crm.Api.Middleware;

public sealed class GlobalExceptionMiddleware(RequestDelegate next,ILogger<GlobalExceptionMiddleware> logger)
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

    private static async Task HandleExceptionAsync( HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";

        switch (exception)
        {
            case ValidationException validationException:
                {
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;

                    var errors = validationException.Errors
                        .GroupBy(x => x.PropertyName)
                        .ToDictionary(
                            group => group.Key,
                            group => group
                                .Select(x => x.ErrorMessage)
                                .ToArray());

                    var response = new ApiResponse<object>
                    {
                        Success = false,
                        StatusCode = StatusCodes.Status400BadRequest,
                        Message = "اطلاعات وارد شده معتبر نیست.",
                        Errors = errors
                    };

                    await context.Response.WriteAsJsonAsync(response);
                    return;
                }

            case UnauthorizedException:
                {
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;

                    var response = new ApiResponse<object>
                    {
                        Success = false,
                        StatusCode = StatusCodes.Status401Unauthorized,
                        Message = exception.Message
                    };

                    await context.Response.WriteAsJsonAsync(response);
                    return;
                }

            case ForbiddenException:
                {
                    context.Response.StatusCode = StatusCodes.Status403Forbidden;

                    var response = new ApiResponse<object>
                    {
                        Success = false,
                        StatusCode = StatusCodes.Status403Forbidden,
                        Message = exception.Message
                    };

                    await context.Response.WriteAsJsonAsync(response);
                    return;
                }

            case NotFoundException:
                {
                    context.Response.StatusCode = StatusCodes.Status404NotFound;

                    var response = new ApiResponse<object>
                    {
                        Success = false,
                        StatusCode = StatusCodes.Status404NotFound,
                        Message = exception.Message
                    };

                    await context.Response.WriteAsJsonAsync(response);
                    return;
                }

            case ConflictException:
                {
                    context.Response.StatusCode = StatusCodes.Status409Conflict;

                    var response = new ApiResponse<object>
                    {
                        Success = false,
                        StatusCode = StatusCodes.Status409Conflict,
                        Message = exception.Message
                    };

                    await context.Response.WriteAsJsonAsync(response);
                    return;
                }

            default:
                {
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;

                    var response = new ApiResponse<object>
                    {
                        Success = false,
                        StatusCode = StatusCodes.Status500InternalServerError,
                        Message = "خطای داخلی سرور رخ داده است."
                    };

                    await context.Response.WriteAsJsonAsync(response);
                    return;
                }
        }
    }
}