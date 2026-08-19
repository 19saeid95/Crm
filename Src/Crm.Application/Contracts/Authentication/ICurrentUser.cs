namespace Crm.Application.Contracts.Authentication;

public interface ICurrentUser
{
    long UserId { get; }

    string? UserName { get; }

    bool IsAuthenticated { get; }
}
