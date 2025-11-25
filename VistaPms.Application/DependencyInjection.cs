using System.Reflection;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;


namespace VistaPms.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        var assembly = Assembly.GetExecutingAssembly();

        // MediatR
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(assembly);
            
            // Pipeline Behaviors
            cfg.AddOpenBehavior(typeof(Common.Behaviors.ValidationBehavior<,>));
            cfg.AddOpenBehavior(typeof(Common.Behaviors.LoggingBehavior<,>));
            cfg.AddOpenBehavior(typeof(Common.Behaviors.PerformanceBehavior<,>));
        });

        // FluentValidation
        services.AddValidatorsFromAssembly(assembly);



        return services;
    }
}
