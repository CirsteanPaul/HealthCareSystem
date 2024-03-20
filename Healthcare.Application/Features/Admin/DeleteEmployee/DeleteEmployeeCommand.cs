using Healthcare.Application.Core.Abstractions.Messaging;

namespace Healthcare.Application.Features.Admin.DeleteEmployee;

public record DeleteEmployeeCommand(Guid UserId) : ICommand;