using Healthcare.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Healthcare.Infrastructure.Configurations;

public class InvestigationTypeConfiguration : IEntityTypeConfiguration<InvestigationType>
{
    public void Configure(EntityTypeBuilder<InvestigationType> builder)
    {
        builder.HasKey(i => i.Id);
    }
}