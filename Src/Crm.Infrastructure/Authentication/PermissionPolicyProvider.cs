using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

namespace Crm.Infrastructure.Authorization;

public sealed class PermissionPolicyProvider
    : IAuthorizationPolicyProvider
{
    private const string Prefix = "Permission.";

    private readonly DefaultAuthorizationPolicyProvider fallbackPolicyProvider;

    public PermissionPolicyProvider(
        IOptions<AuthorizationOptions> options)
    {
        fallbackPolicyProvider =
            new DefaultAuthorizationPolicyProvider(options);
    }

    public Task<AuthorizationPolicy?> GetPolicyAsync(
        string policyName)
    {
        if (!policyName.StartsWith(
                Prefix,
                StringComparison.OrdinalIgnoreCase))
        {
            return fallbackPolicyProvider
                .GetPolicyAsync(policyName);
        }

        var permission =
            policyName[Prefix.Length..];

        var policy =
            new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .AddRequirements(
                    new PermissionRequirement(permission))
                .Build();

        return Task.FromResult<AuthorizationPolicy?>(
            policy);
    }

    public Task<AuthorizationPolicy>
        GetDefaultPolicyAsync()
    {
        return fallbackPolicyProvider
            .GetDefaultPolicyAsync();
    }

    public Task<AuthorizationPolicy?>
        GetFallbackPolicyAsync()
    {
        return fallbackPolicyProvider
            .GetFallbackPolicyAsync();
    }
}