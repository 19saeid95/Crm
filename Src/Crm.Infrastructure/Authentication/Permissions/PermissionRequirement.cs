using Microsoft.AspNetCore.Authorization;

namespace Crm.Infrastructure.Authentication;

public sealed class PermissionRequirement(string permission) : IAuthorizationRequirement
{
    public string Permission { get; } = permission;
}
