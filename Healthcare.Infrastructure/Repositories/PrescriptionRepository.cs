using Healthcare.Application.Core.Abstractions.Data;
using Healthcare.Domain.Entities;

namespace Healthcare.Infrastructure.Repositories;

public sealed class PrescriptionRepository : AsyncRepository<Prescription>, IPrescriptionRepository
{
    public PrescriptionRepository(HealthcareContext context) : base(context)
    {
    }
}