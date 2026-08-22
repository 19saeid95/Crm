namespace Crm.Api.Models;

public static class ApiResponseFactory
{
    public static ApiResponse<T> Success<T>(
        T data,
        int statusCode = StatusCodes.Status200OK,
        string? message = null)
    {
        return new ApiResponse<T>
        {
            Success = true,
            StatusCode = statusCode,
            Message = message,
            Data = data
        };
    }
}