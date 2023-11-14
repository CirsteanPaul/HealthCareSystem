using Healthcare.Application.Core.Abstractions.Messaging;

namespace Healthcare.Application.Users.Login;

public record UserLoginCommand(string Cnp, string Password): ICommand<UserLoginResponse>;