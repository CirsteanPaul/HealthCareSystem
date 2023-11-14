using System.Reflection;
using Healthcare.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Healthcare.Infrastructure;

public class HealthcareContext : DbContext
{
    public DbSet<Test> Tests { get; set; }
    public DbSet<User> Users { get; set; }
   
    public HealthcareContext(DbContextOptions options)
        : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        
        base.OnModelCreating(modelBuilder);
    }
}

// public class BloggingContextFactory : IDesignTimeDbContextFactory<HealthcareContext>
// {
//     private readonly string? _connectionString;
//     public BloggingContextFactory(string? connectionString)
//     {
//         _connectionString = connectionString;
//     }
//     
//     public BloggingContextFactory() { }
//     public HealthcareContext CreateDbContext(string[] args)
//     {
//         var optionsBuilder = new DbContextOptionsBuilder<HealthcareContext>();
//         optionsBuilder.UseNpgsql(_connectionString);
//
//         return new HealthcareContext(optionsBuilder.Options);
//     }
// }