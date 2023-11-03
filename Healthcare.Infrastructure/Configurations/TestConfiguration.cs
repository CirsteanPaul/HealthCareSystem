using Healthcare.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Healthcare.Infrastructure.Configurations;

public sealed class TestConfiguration : IEntityTypeConfiguration<Domain.Test>
{
    public void Configure(EntityTypeBuilder<Test> builder)
    {
        builder.HasKey(t => t.Id);

        builder.Property(t => t.Code).HasDefaultValue(0);
    }
}