using Healthcare.Domain.Errors;
using Healthcare.Domain.Shared.EntityTypes;
using Healthcare.Domain.Shared.Results;

namespace Healthcare.Domain.Entities;

public sealed class MedicalReport : AggregateRoot, IAuditableEntity
{
    public Guid DoctorId { get; private set; }
    public Guid AppointmentId { get; private set; }
    public string? Conclusion { get; private set; }
    
    public bool IsFinished { get; private set; }
    public Guid? PrescriptionId { get; private set; }
    public List<Investigation> Investigations { get; private set; }
    
    public Guid? CreatedBy { get; set; }
    public DateTime CreatedDate { get; set; }
    public Guid? LastModifiedBy { get; set; }
    public DateTime LastModifiedDate { get; set; }
    
    private MedicalReport() { }

    private MedicalReport(Guid doctorId, Guid appointmentId) : base(Guid.NewGuid())
    {
        DoctorId = doctorId;
        AppointmentId = appointmentId;
        Conclusion = null;
        CreatedBy = doctorId;
        CreatedDate = DateTime.UtcNow;
        LastModifiedBy = doctorId;
        LastModifiedDate = DateTime.UtcNow;
        PrescriptionId = null;
        Investigations = new();
    }

    public static Result<MedicalReport> Create(Guid doctorId, Guid appointmentId)
    {
        if (doctorId == Guid.Empty || appointmentId == Guid.Empty)
        {
            return Result.Failure<MedicalReport>(DomainErrors.General.InternalServerError);
        }

        MedicalReport newMedicalReport = new(doctorId, appointmentId);
        
        // TODO: Raise domain event.

        return newMedicalReport;
    }

    public void AddInvestigation(Investigation investigation)
    {
        Investigations.Add(investigation);
    }

    public Result RemoveInvestigation(Guid investigationId)
    {
        var investigation = Investigations.FirstOrDefault(i => i.Id == investigationId);

        if (investigation is null)
        {
            return Result.Failure(DomainErrors.General.InternalServerError);
        }
        
        Investigations.Remove(investigation);

        return Result.Success();
    }

    public Result AddNewPrescription()
    {
        if (PrescriptionId is not null)
        {
            return Result.Failure(DomainErrors.General.InternalServerError);
        }
        
        // raise domain event. Guid MedicalReport

        return Result.Success();
    }

    public Result FinishMedicalReport()
    {
        if (IsFinished)
        {
            return Result.Failure(DomainErrors.General.InternalServerError);
        }

        IsFinished = true;
        
        // Todo: Raise domain event to change Appointment status.

        return Result.Success();
    }
}