using Healthcare.Application.Core.Abstractions.Data;
using Healthcare.Domain.Entities;

namespace Healthcare.Infrastructure.Repositories;

public sealed class InvestigationTypeRepository : AsyncRepository<InvestigationType>, IInvestigationTypeRepository
{
    public InvestigationTypeRepository(HealthcareContext context) : base(context)
    {
    }
}