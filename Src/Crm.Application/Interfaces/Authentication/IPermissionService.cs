namespace Crm.Application.Interfaces.Authorization;

public interface IPermissionService
{
    Task<bool> HasPermissionAsync(
        long userId,
        string permission,
        CancellationToken cancellationToken = default);
}