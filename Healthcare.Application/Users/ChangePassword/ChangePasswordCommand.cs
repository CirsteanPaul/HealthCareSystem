using Healthcare.Application.Core.Abstractions.Messaging;

namespace Healthcare.Application.Users.ChangePassword;

public record ChangePasswordCommand(Guid UserId, string NewPassword) : ICommand;