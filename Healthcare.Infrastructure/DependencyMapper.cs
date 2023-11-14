using System.Text;
using Healthcare.Application.Core.Abstractions.Authentication;
using Healthcare.Application.Core.Abstractions.Cryptography;
using Healthcare.Application.Core.Abstractions.Data;
using Healthcare.Infrastructure.Authentication;
using Healthcare.Infrastructure.Authentication.Settings;
using Healthcare.Infrastructure.Cryptography;
using Healthcare.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
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
        services.AddAuthentication(configuration);
        
        // options validations
        services.AddOptions<JwtSettings>()
            .Bind(configuration.GetSection(JwtSettings.SettingsKey))
            .ValidateDataAnnotations()
            .ValidateOnStart();

        services.AddScoped<IJwtProvider, JwtProvider>();
        services.AddScoped<IUserIdentityProvider, UserIdentityProvider>();

        services.AddDbContext<HealthcareContext>(o => o.UseNpgsql(connectionString));
        services.AddScoped<ITestRepository, TestRepository>();
        services.AddScoped<IPasswordHasher, PasswordHasher>();
        services.AddScoped<IUserRepository, UserRepository>();
        return services;
    }

    private static IServiceCollection AddAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
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