using System.Text;
using Healthcare.Application.Core.Abstractions.Authentication;
using Healthcare.Application.Core.Abstractions.Data;
using Healthcare.Infrastructure.Authentication;
using Healthcare.Infrastructure.Authentication.Settings;
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
        services.AddHttpContextAccessor();
        // services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        // options validations
        services.AddOptions<JwtSettings>()
            .Bind(configuration.GetSection(JwtSettings.SettingsKey))
            .ValidateDataAnnotations()
            .ValidateOnStart();

        services.AddScoped<IJwtProvider, JwtProvider>();
    
        services.AddScoped<IUserIdentityProvider, UserIdentityProvider>();
        
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

        services.AddDbContext<HealthcareContext>(options =>
            options.UseNpgsql(
                "USER ID=paul;Password=Complicated;HOST=localhost;PORT=5432;Database=Healthcare;Pooling=true;"));
        services.AddScoped<ITestRepository, TestRepository>();
        return services;
    }
}