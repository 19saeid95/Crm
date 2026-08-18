using Crm.Domain.Repositories;
using Crm.Domain.Repositories.Generics;
using Crm.Domain.Services;
using Crm.Infrastructure.Persistence;
using Crm.Infrastructure.Repositories;
using Crm.Infrastructure.Repositories.Generics;
using Crm.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

public static class InfrastructureServiceExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        //sqlServer
        services.AddDbContext<CrmDbContext>(option => { option.UseSqlServer(configuration.GetConnectionString("CrmDb")); });

        services.AddScoped(typeof(IRepository<,>), typeof(Repository<,>));
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IPasswordHasher, PasswordHasher>();


        return services;
    }
}
