using Healthcare.Application.Core.Abstractions.Messaging;

namespace Healthcare.Application.Features.Users.ChangeDetails;

public record UserChangeDetailsCommand(string Email, string PhoneNumber) : ICommand;