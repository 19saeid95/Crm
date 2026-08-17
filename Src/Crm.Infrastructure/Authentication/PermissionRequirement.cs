using Microsoft.AspNetCore.Authorization;

namespace Crm.Infrastructure.Authorization;

public sealed class PermissionRequirement(
    string permission)
    : IAuthorizationRequirement
{
    public string Permission { get; } = permission;
}