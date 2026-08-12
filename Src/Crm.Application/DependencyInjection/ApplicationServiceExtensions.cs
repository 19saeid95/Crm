using Microsoft.Extensions.DependencyInjection;

namespace Crm.Application.DependencyInjection;

public static class ApplicationServiceExtensions
{
    public static IServiceCollection AddApplication(
        this IServiceCollection services)
    {
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(
                typeof(ApplicationServiceExtensions).Assembly);
        });

        //services.AddValidatorsFromAssembly(
        //    typeof(ApplicationServiceExtensions).Assembly);

        return services;
    }
}