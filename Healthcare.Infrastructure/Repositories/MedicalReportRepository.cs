using Healthcare.Application.Core.Abstractions.Data;
using Healthcare.Domain.Entities;

namespace Healthcare.Infrastructure.Repositories;

public sealed class MedicalReportRepository : AsyncRepository<MedicalReport>, IMedicalReportRepository
{
    public MedicalReportRepository(HealthcareContext context) : base(context)
    {
    }
}