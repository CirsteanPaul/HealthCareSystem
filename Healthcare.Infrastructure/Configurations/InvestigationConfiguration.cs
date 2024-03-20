using Healthcare.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Healthcare.Infrastructure.Configurations;

public sealed class InvestigationConfiguration : IEntityTypeConfiguration<Investigation>
{
    public void Configure(EntityTypeBuilder<Investigation> builder)
    {
        builder.HasKey(i => i.Id);
        
        builder.HasOne<MedicalReport>()
            .WithMany()
            .HasForeignKey(a => a.MedicalReportId)
            .IsRequired()
            .OnDelete(DeleteBehavior.NoAction);
        
        builder.HasOne<InvestigationType>()
            .WithMany()
            .HasForeignKey(a => a.InvestigationTypeId)
            .IsRequired()
            .OnDelete(DeleteBehavior.NoAction);
    }
}