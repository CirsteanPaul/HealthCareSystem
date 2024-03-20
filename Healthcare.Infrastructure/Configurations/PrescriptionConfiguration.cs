using Healthcare.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Healthcare.Infrastructure.Configurations;

public class PrescriptionConfiguration : IEntityTypeConfiguration<Prescription>
{
    public void Configure(EntityTypeBuilder<Prescription> builder)
    {
        builder.HasKey(p => p.Id);
        
        builder.HasOne<MedicalReport>()
            .WithMany()
            .HasForeignKey(a => a.MedicalReportId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}