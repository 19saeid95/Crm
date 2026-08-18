using Crm.Domain.Entities;
using Crm.Domain.Services;
using Microsoft.AspNetCore.Identity;

namespace Crm.Infrastructure.Services;

public class PasswordHasher : IPasswordHasher
{
    private readonly IPasswordHasher<User> _passwordHasher = new PasswordHasher<User>();

    public string Hash(string password)
    {
        return _passwordHasher.HashPassword( new User(), password);
    }

    public bool Verify(string password,string passwordHash)
    {
        var result = _passwordHasher.VerifyHashedPassword( new User(), passwordHash, password);

        return result == PasswordVerificationResult.Success;
    }
}

