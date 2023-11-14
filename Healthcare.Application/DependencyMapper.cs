using System.Reflection;
using FluentValidation;
using Healthcare.Application.Core.Behaviors;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Healthcare.Application;

public static class DependencyMapper
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(cfg=>cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehavior<,>));
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly(), includeInternalTypes: true);
        
        return services;
    }
}