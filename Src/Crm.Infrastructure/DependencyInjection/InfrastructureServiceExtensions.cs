using Crm.Application.Interfaces.Authentication;
using Crm.Application.Interfaces.Authorization;
using Crm.Domain.Repositories;
using Crm.Domain.Repositories.Generics;
using Crm.Infrastructure.Authentication;
using Crm.Infrastructure.Authorization;
using Crm.Infrastructure.Persistence;
using Crm.Infrastructure.Repositories;
using Crm.Infrastructure.Repositories.Generics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace Crm.Infrastructure.DependencyInjection;

public static class InfrastructureServiceExtensions
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<CrmDbContext>(options =>
        {
            options.UseSqlServer(
                configuration.GetConnectionString("CrmDb"));
        });

        services.AddScoped(
            typeof(IRepository<,>),
            typeof(Repository<,>));

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddScoped<
            IPasswordHasher,
            PasswordHasher>();

        services.AddScoped<
            IJwtTokenGenerator,
            JwtTokenGenerator>();

        services.AddScoped<
            IAuthenticationTokenService,
            AuthenticationTokenService>();

        services.AddScoped<IUserRepository, UserRepository>();

        services.AddScoped<IPermissionService, PermissionService>();

        services.AddScoped<IPermissionService, PermissionService>();

        services.AddScoped< IAuthorizationHandler, PermissionAuthorizationHandler>();

        services.AddSingleton<
            IAuthorizationPolicyProvider,
            PermissionPolicyProvider>();

        var redisConnectionString =
            configuration["Redis:ConnectionString"]
            ?? throw new InvalidOperationException(
             "Redis:ConnectionString is not configured.");

        services.AddSingleton<IConnectionMultiplexer>(
            _ => ConnectionMultiplexer.Connect(
                redisConnectionString));

        services.AddScoped<IRefreshTokenService,
            RedisRefreshTokenService>();

        return services;
    }
}