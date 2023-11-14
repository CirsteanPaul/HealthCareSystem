using Healthcare.Application.Core.Abstractions.Messaging;

namespace Healthcare.Application.Admin.DeleteEmployee;

public record DeleteEmployeeCommand(Guid UserId) : ICommand;