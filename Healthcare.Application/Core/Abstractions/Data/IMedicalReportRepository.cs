using Healthcare.Domain.Entities;

namespace Healthcare.Application.Core.Abstractions.Data;

public interface IMedicalReportRepository : IAsyncRepository<MedicalReport>
{
    
}