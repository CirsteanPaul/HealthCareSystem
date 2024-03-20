using Healthcare.Application.Core.Abstractions.Cryptography;
using Healthcare.Application.Core.Abstractions.Data;
using Healthcare.Application.Core.Abstractions.Messaging;
using Healthcare.Domain.Entities;
using Healthcare.Domain.Errors;
using Healthcare.Domain.Shared.Results;
using Healthcare.Domain.ValueObjects;

namespace Healthcare.Application.Features.Registratur.CreateUser;

public sealed class CreateUserCommandHandler : ICommandHandler<CreateUserCommand, CreateUserResponse>
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;

    public CreateUserCommandHandler(IUserRepository userRepository, IPasswordHasher passwordHasher)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
    }

    public async Task<Result<CreateUserResponse>> Handle(CreateUserCommand command, CancellationToken cancellationToken)
    {
        var cnpResult = Cnp.Create(command.Cnp);

        if (cnpResult.IsFailure)
        {
            return Result.Failure<CreateUserResponse>(DomainErrors.User.CnpInvalidError);
        }
        
        var userWithSameCnp = await _userRepository.GetUserByCnp(cnpResult.Value);

        if (userWithSameCnp.IsSuccess)
        {
            return Result.Failure<CreateUserResponse>(DomainErrors.User.SameCnpError);
        }

        var passwordResult = _passwordHasher.HashPassword("abcdefghij");

        if (passwordResult.IsFailure)
        {
            return Result.Failure<CreateUserResponse>(DomainErrors.General.PasswordCannotBeHashedError);
        }

        var userResult = User.Create(command.Cnp, passwordResult.Value, command.Email, command.PhoneNumber, UserPermission.Patient);

        if (userResult.IsFailure)
        {
            return Result.Failure<CreateUserResponse>(userResult.Error);
        }

        await _userRepository.AddAsync(userResult.Value);

        var response = new CreateUserResponse() { Id = userResult.Value.Id };
        return Result.Success(response);
    }
}