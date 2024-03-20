using Healthcare.Application.Core.Abstractions.Messaging;

namespace Healthcare.Application.Features.Registratur.CreateUser;

public record CreateUserCommand(string Cnp, string PhoneNumber, string Email) : ICommand<CreateUserResponse>;