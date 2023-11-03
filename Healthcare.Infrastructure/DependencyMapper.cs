using Healthcare.Application.Core.Abstractions.Data;
using Healthcare.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Healthcare.Infrastructure;

public static class DependencyMapper
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<HealthcareContext>(options =>
            options.UseNpgsql(
                "USER ID=paul;Password=Complicated;HOST=localhost;PORT=5432;Database=Healthcare;Pooling=true;"));
        services.AddScoped<ITestRepository, TestRepository>();
        return services;
    }
}