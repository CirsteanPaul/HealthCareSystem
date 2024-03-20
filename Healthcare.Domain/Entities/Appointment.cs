using Healthcare.Domain.Errors;
using Healthcare.Domain.Shared.EntityTypes;
using Healthcare.Domain.Shared.Results;

namespace Healthcare.Domain.Entities;

public enum AppointmentStatus
{
    InPending,
    InWaiting,
    Done,
    Paid,
    Cancelled = 1001,
}
public class Appointment : AggregateRoot, IAuditableEntity
{
    public Guid RegistraturId { get; private set; }
    public Guid PacientId { get; private set; }
    public Guid DoctorId { get; private set; }
    public Guid? MedicalReportId { get; private set; }
    public AppointmentStatus Status { get; private set; }
    public string? ReasonOfCancellation { get; private set; }
    public string? AppointmentDetails { get; private set; }
    public DateTime DueTime { get; set; }
    
    public Guid? CreatedBy { get; set; }
    public DateTime CreatedDate { get; set; }
    public Guid? LastModifiedBy { get; set; }
    public DateTime LastModifiedDate { get; set; }
    
    private Appointment() { }

    private Appointment(Guid registraturId, Guid pacientId, Guid doctorId, DateTime dueTime) : base(Guid.NewGuid())
    {
        RegistraturId = registraturId;
        PacientId = pacientId;
        DoctorId = doctorId;
        DueTime = dueTime;
        Status = AppointmentStatus.InPending;
        MedicalReportId = null;
        CreatedBy = registraturId;
        CreatedDate = DateTime.UtcNow;
        LastModifiedBy = registraturId;
        LastModifiedDate = DateTime.UtcNow;
    }

    public static Result<Appointment> Create(Guid registraturId, Guid pacientId, Guid doctorId, DateTime dueTime)
    {
        if (registraturId == Guid.Empty || pacientId == Guid.Empty || doctorId == Guid.Empty)
        {
            return Result.Failure<Appointment>(DomainErrors.General.EntityNotFoundError);
        }
        
        Appointment newAppointment = new(registraturId, pacientId, doctorId, dueTime);
        
        newAppointment.TryChangeStatusToWaiting();
        
        // TODO: Raise domain event
        
        return newAppointment;
    }

    public Result TryChangeStatusToWaiting()
    {
        var delta = DueTime - DateTime.UtcNow;
        
        if (delta.Days < 2)
        {
            this.Status = AppointmentStatus.InWaiting;
            return Result.Success();
        }

        return Result.Failure(DomainErrors.General.InternalServerError);
    }

    public Result CancelAppointment(string reason)
    {
        if (Status != AppointmentStatus.InWaiting)
        {
            return Result.Failure(DomainErrors.General.InternalServerError);
        }

        ReasonOfCancellation = reason;
        Status = AppointmentStatus.Cancelled;

        return Result.Success();
    }

    public Result ChangeAppointmentDetails(string details)
    {
        if (Status >= AppointmentStatus.Done)
        {
            return Result.Failure(DomainErrors.General.InternalServerError);
        }

        AppointmentDetails = details;

        return Result.Success();
    }
}