using Healthcare.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Healthcare.Infrastructure.Configurations;

public class AppointmentConfiguration : IEntityTypeConfiguration<Appointment>
{
    public void Configure(EntityTypeBuilder<Appointment> builder)
    {
        builder.HasKey(a => a.Id);

        builder.HasOne<User>()
            .WithMany()
            .HasForeignKey(a => a.DoctorId)
            .IsRequired()
            .OnDelete(DeleteBehavior.NoAction);
        
        builder.HasOne<User>()
            .WithMany()
            .HasForeignKey(a => a.RegistraturId)
            .IsRequired()
            .OnDelete(DeleteBehavior.NoAction);
        
        builder.HasOne<User>()
            .WithMany()
            .HasForeignKey(a => a.PacientId)
            .IsRequired()
            .OnDelete(DeleteBehavior.NoAction);
        
        builder.HasOne<MedicalReport>()
            .WithMany()
            .HasForeignKey(a => a.MedicalReportId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}