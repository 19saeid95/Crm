using Microsoft.Extensions.DependencyInjection;
using FluentValidation;
using Crm.Application.Common.Behaviors;

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

            config.AddOpenBehavior(
                typeof(ValidationBehavior<,>));
        });

        services.AddValidatorsFromAssembly(
            typeof(ApplicationServiceExtensions).Assembly);


        services.AddAutoMapper(
            config => { },
            typeof(ApplicationServiceExtensions).Assembly
        );


        return services;
    }
}