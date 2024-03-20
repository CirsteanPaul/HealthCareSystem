using Healthcare.Domain.Errors;
using Healthcare.Domain.Shared.EntityTypes;
using Healthcare.Domain.Shared.Results;

namespace Healthcare.Domain.Entities;

public sealed class Investigation : Entity, IAuditableEntity
{
    public Guid MedicalReportId { get; private set; }
    public Guid InvestigationTypeId { get; private set; }
    public string Content { get; private set; }
    public string Conclusion { get; private set; }
    
    public Guid? CreatedBy { get; set; }
    public DateTime CreatedDate { get; set; }
    public Guid? LastModifiedBy { get; set; }
    public DateTime LastModifiedDate { get; set; }

    private Investigation() {}

    private Investigation(Guid medicalReportId, Guid investigationTypeId, string content, string conclusion)
        : base(Guid.NewGuid())
    {
        MedicalReportId = medicalReportId;
        InvestigationTypeId = investigationTypeId;
        Content = content;
        Conclusion = conclusion;
    }
    
    public static Result<Investigation> Create(Guid medicalReportId, Guid investigationTypeId, string content, string conclusion)
    {
        if (string.IsNullOrWhiteSpace(content))
        {
            return Result.Failure<Investigation>(DomainErrors.General.InternalServerError);
        }
        
        if (string.IsNullOrWhiteSpace(conclusion))
        {
            return Result.Failure<Investigation>(DomainErrors.General.InternalServerError);
        }

        Investigation newInvestigation = new(medicalReportId, investigationTypeId, content, conclusion);

        return newInvestigation;
    }
}