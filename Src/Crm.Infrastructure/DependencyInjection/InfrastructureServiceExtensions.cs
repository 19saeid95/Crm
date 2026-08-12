using Crm.Application.Interfaces.Authentication;
using Crm.Domain.Repositories;
using Crm.Domain.Repositories.Generics;
using Crm.Infrastructure.Authentication;
using Crm.Infrastructure.Persistence;
using Crm.Infrastructure.Repositories;
using Crm.Infrastructure.Repositories.Generics;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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

        services.AddScoped<IUserRepository, UserRepository>();

        return services;
    }
}