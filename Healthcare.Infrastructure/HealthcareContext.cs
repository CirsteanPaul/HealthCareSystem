using System.Reflection;
using Healthcare.Domain.Entities;
using Healthcare.Domain.Shared.EntityTypes;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Healthcare.Infrastructure;

public class HealthcareContext : DbContext
{
    private readonly IMediator _mediator;
    public DbSet<Test> Tests { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Appointment> Appointments { get; set; }
    public DbSet<MedicalReport> MedicalReports { get; set; }
    public DbSet<Investigation> Investigations { get; set; }
    public DbSet<InvestigationType> InvestigationTypes { get; set; }
    public DbSet<Prescription> Prescriptions { get; set; }
   
    public HealthcareContext(DbContextOptions options, IMediator mediator)
        : base(options)
    {
        _mediator = mediator;
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        
        base.OnModelCreating(modelBuilder);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await PublishDomainEvents(cancellationToken);

        return await base.SaveChangesAsync(cancellationToken);
    }

    private async Task PublishDomainEvents(CancellationToken cancellationToken)
    { 
        var aggregateRoots = ChangeTracker
            .Entries<AggregateRoot>()
            .Where(entityEntry => entityEntry.Entity.DomainEvents.Any())
            .ToList();

        var domainEvents = aggregateRoots.SelectMany(entityEntry => entityEntry.Entity.DomainEvents).ToList();

        aggregateRoots.ForEach(entityEntry => entityEntry.Entity.ClearDomainEvents());

        var tasks = domainEvents.Select(domainEvent => _mediator.Publish(domainEvent, cancellationToken));

        await Task.WhenAll(tasks);
    }
}