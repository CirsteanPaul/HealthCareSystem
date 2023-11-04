using System.Reflection;
using Healthcare.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Healthcare.Infrastructure;

public class HealthcareContext : DbContext
{
    public DbSet<Test> Tests { get; set; }
   
    public HealthcareContext(DbContextOptions options)
        : base(options)
    {
    }
    
    
    // protected override void OnConfiguring(DbContextOptionsBuilder options)
    // {
    //     options.UseNpgsql(
    //         "USER ID=paul;Password=Complicated;HOST=localhost;PORT=5432;Database=Healthcare;Pooling=true;"));
    // }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        
        base.OnModelCreating(modelBuilder);
    }
}