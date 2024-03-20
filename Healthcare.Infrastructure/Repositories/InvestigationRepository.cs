using Healthcare.Application.Core.Abstractions.Data;
using Healthcare.Domain.Entities;

namespace Healthcare.Infrastructure.Repositories;

public sealed class InvestigationRepository : AsyncRepository<Investigation>, IInvestigationRepository
{
    public InvestigationRepository(HealthcareContext context) : base(context)
    {
    }
}