using Healthcare.Domain.Entities;
using Healthcare.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Healthcare.Infrastructure.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(u => u.Id);

        builder.Property<string>("_hashedPassword")
            .HasField("_hashedPassword")
            .HasColumnName("HashedPassword")
            .IsRequired();

        builder.OwnsOne(u => u.Email, emailBuilder =>
        {
            emailBuilder.WithOwner();

            emailBuilder.Property(e => e.Value)
                .HasColumnName(nameof(User.Email))
                .HasMaxLength(Email.MaxLength)
                .IsRequired();
        });
        
        builder.OwnsOne(u => u.Cnp, cnpBuilder =>
        {
            cnpBuilder.WithOwner();
            cnpBuilder.HasIndex(c => c.Value).IsUnique();

            cnpBuilder.Property(e => e.Value)
                .HasColumnName(nameof(User.Cnp))
                .HasMaxLength(Cnp.Length)
                .IsRequired();
        });
        
        builder.OwnsOne(u => u.PhoneNumber, phoneNumber =>
        {
            phoneNumber.WithOwner();

            phoneNumber.Property(e => e.Value)
                .HasColumnName(nameof(User.PhoneNumber))
                .HasMaxLength(30)
                .IsRequired();
        });
    }
}