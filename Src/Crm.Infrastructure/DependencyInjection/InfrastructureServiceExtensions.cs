using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Crm.Domain.Repositories;
using Crm.Infrastructure.Persistence;
using Crm.Infrastructure.Repositories;
using Crm.Domain.Repositories.Generics;
using Crm.Infrastructure.Repositories.Generics;

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

        services.AddScoped(typeof(IRepository<,>),typeof(Repository<,>));

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}