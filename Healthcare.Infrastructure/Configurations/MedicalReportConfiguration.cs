using Healthcare.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Healthcare.Infrastructure.Configurations;

public sealed class MedicalReportConfiguration : IEntityTypeConfiguration<MedicalReport>
{
    public void Configure(EntityTypeBuilder<MedicalReport> builder)
    {
        builder.HasKey(m => m.Id);
        
        builder.HasOne<User>()
            .WithMany()
            .HasForeignKey(a => a.DoctorId)
            .IsRequired()
            .OnDelete(DeleteBehavior.NoAction);
        
        builder.HasOne<Appointment>()
            .WithMany()
            .HasForeignKey(a => a.AppointmentId)
            .IsRequired()
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasMany(a => a.Investigations)
            .WithOne()
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne<Prescription>()
            .WithMany()
            .HasForeignKey(a => a.PrescriptionId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}