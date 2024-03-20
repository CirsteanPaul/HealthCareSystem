using Healthcare.Domain.Errors;
using Healthcare.Domain.Shared.EntityTypes;
using Healthcare.Domain.Shared.Results;

namespace Healthcare.Domain.Entities;

public sealed class Prescription : AggregateRoot, IAuditableEntity
{
    public Guid MedicalReportId { get; private set; }
    public bool IsTaken { get; private set; }
    // public List<Medicine> Medicines { get; private set; }
    public Guid? CreatedBy { get; set; }
    public DateTime CreatedDate { get; set; }
    public Guid? LastModifiedBy { get; set; }
    public DateTime LastModifiedDate { get; set; }
    
    private Prescription() { }

    private Prescription(Guid medicalReportId) : base(Guid.NewGuid())
    {
        MedicalReportId = medicalReportId;
        IsTaken = false;
        CreatedDate = DateTime.UtcNow;
        LastModifiedDate = DateTime.UtcNow;
    }

    public static Result<Prescription> Create(Guid medicalReportId)
    {
        if (medicalReportId == Guid.Empty)
        {
            return Result.Failure<Prescription>(DomainErrors.General.InternalServerError);
        }

        var newPrescription = new Prescription(medicalReportId);

        return newPrescription;
    }
}