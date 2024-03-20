using System.Text;
using Healthcare.Application.Core.Abstractions.Authentication;
using Healthcare.Application.Core.Abstractions.Cryptography;
using Healthcare.Application.Core.Abstractions.Data;
using Healthcare.Application.Core.Abstractions.Email;
using Healthcare.Application.Core.Abstractions.Messaging;
using Healthcare.Application.Core.Notifications;
using Healthcare.Infrastructure.Authentication;
using Healthcare.Infrastructure.Authentication.Settings;
using Healthcare.Infrastructure.Cryptography;
using Healthcare.Infrastructure.Emails;
using Healthcare.Infrastructure.Messaging;
using Healthcare.Infrastructure.Notifications;
using Healthcare.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Healthcare.Infrastructure;

public static class DependencyMapper
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("HealthCareDb");
        
        services.AddHttpContextAccessor();
        
        services.AddOptions<MessageBrokerSettings>()
            .Bind(configuration.GetSection(MessageBrokerSettings.SettingsKey))
            .ValidateDataAnnotations()
            .ValidateOnStart();
        
        services.AddOptions<MailSettings>()
            .Bind(configuration.GetSection(MailSettings.SettingsKey))
            .ValidateDataAnnotations()
            .ValidateOnStart();

        services.AddScoped<IIntegrationEventPublisher, IntegrationEventPublisher>();

        services.AddScoped<IEmailSmtp, EmailSmtp>();

        services.AddScoped<IEmailNotificationService, EmailNotificationService>();
        services.AddScoped<IJwtProvider, JwtProvider>();
        services.AddScoped<UserIdentityProvider>();
        services.AddScoped<IUserIdentityProvider>(provider => provider.GetRequiredService<UserIdentityProvider>());
        services.AddScoped<IIdentityService>(provider => provider.GetRequiredService<UserIdentityProvider>());

        services.AddDbContext<HealthcareContext>(o => o.UseNpgsql(connectionString));
        services.AddScoped<IAppointmentRepository, AppointmentRepository>();
        services.AddScoped<ITestRepository, TestRepository>();
        services.AddScoped<IPasswordHasher, PasswordHasher>();
        services.AddScoped<IMedicalReportRepository, MedicalReportRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IInvestigationRepository, InvestigationRepository>();
        services.AddScoped<IInvestigationTypeRepository, InvestigationTypeRepository>();
        services.AddScoped<IMedicalReportRepository, MedicalReportRepository>();
        services.AddScoped<IPrescriptionRepository, PrescriptionRepository>();
        return services;
    }

    public static IServiceCollection AddAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        // options validations
        services.AddOptions<JwtSettings>()
            .Bind(configuration.GetSection(JwtSettings.SettingsKey))
            .ValidateDataAnnotations()
            .ValidateOnStart();
        
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = configuration["Jwt:Issuer"],
                ValidAudience = configuration["Jwt:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(configuration["Jwt:Key"]!))
            });

        return services;
    }
}