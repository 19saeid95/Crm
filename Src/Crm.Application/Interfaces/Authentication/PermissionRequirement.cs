using Microsoft.AspNetCore.Authorization;

namespace Crm.Application.Authorization;

public sealed class PermissionRequirement(
    string permission)
    : IAuthorizationRequirement
{
    public string Permission { get; } = permission;
}