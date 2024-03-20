using Healthcare.Application.Core.Abstractions.Data;
using Healthcare.Domain.Entities;

namespace Healthcare.Infrastructure.Repositories;

public sealed class AppointmentRepository : AsyncRepository<Appointment>, IAppointmentRepository
{
    public AppointmentRepository(HealthcareContext context) : base(context)
    {
    }
}