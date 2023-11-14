using Healthcare.Application.Core.Abstractions.Messaging;

namespace Healthcare.Application.Users.ChangeDetails;

public record UserChangeDetailsCommand(string Email, string PhoneNumber) : ICommand;