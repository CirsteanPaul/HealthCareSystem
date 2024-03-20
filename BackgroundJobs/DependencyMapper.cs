using System.Reflection;
using BackgroundJobs.Services;
using BackgroundJobs.Settings;
using BackgroundJobs.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BackgroundJobs;

public static class DependencyMapper
{
    public static IServiceCollection AddBackgroundTasks(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMediatR(cfg=>cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
        
        services.AddOptions<BackgroundTaskSettings>()
            .Bind(configuration.GetSection(BackgroundTaskSettings.SettingsKey))
            .ValidateDataAnnotations()
            .ValidateOnStart();

        services.AddScoped<IIntegrationEventConsumer, IntegrationEventConsumer>();

        services.AddHostedService<IntegrationEventConsumerBackgroundService>();

        return services;
    }
}