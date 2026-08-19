using Crm.Application.Contracts.Authentication;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Crm.Infrastructure.Authentication;

public sealed class CurrentUser(IHttpContextAccessor httpContextAccessor) : ICurrentUser
{
    private ClaimsPrincipal User => httpContextAccessor.HttpContext?.User ?? new ClaimsPrincipal();

    public long UserId
    {
        get
        {
            var value = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return long.TryParse(value, out var userId) ? userId : 0;
        }
    }
    public string? UserName => User.FindFirstValue(ClaimTypes.Name);

    public bool IsAuthenticated => User.Identity?.IsAuthenticated ?? false;
}
