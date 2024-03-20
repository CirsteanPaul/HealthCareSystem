using Healthcare.Application.Core.Abstractions.Messaging;

namespace Healthcare.Application.Features.Users.Login;

public record UserLoginCommand(string Cnp, string Password): ICommand<UserLoginResponse>;