using Crm.Application.Interfaces.Authentication;
using Crm.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Crm.Infrastructure.Authentication;

public class PasswordHasher : IPasswordHasher
{
    private readonly PasswordHasher<User> _passwordHasher = new();

    public string Hash(string password)
    {
        return _passwordHasher.HashPassword(
            new User(),
            password);
    }

    public bool Verify(
        string password,
        string passwordHash)
    {
        var result = _passwordHasher.VerifyHashedPassword(
            new User(),
            passwordHash,
            password);

        return result == PasswordVerificationResult.Success ||
               result == PasswordVerificationResult.SuccessRehashNeeded;
    }
}