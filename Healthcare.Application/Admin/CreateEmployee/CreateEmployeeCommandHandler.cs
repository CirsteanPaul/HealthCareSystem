using Healthcare.Application.Core.Abstractions.Authentication;
using Healthcare.Application.Core.Abstractions.Cryptography;
using Healthcare.Application.Core.Abstractions.Data;
using Healthcare.Application.Core.Abstractions.Messaging;
using Healthcare.Domain.Entities;
using Healthcare.Domain.Errors;
using Healthcare.Domain.Shared.Results;

namespace Healthcare.Application.Admin.CreateEmployee;

public sealed class CreateEmployeeCommandHandler : ICommandHandler<CreateEmployeeCommand>
{
    private readonly IUserIdentityProvider _userIdentityProvider;
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;

    public CreateEmployeeCommandHandler(IUserIdentityProvider userIdentityProvider, IUserRepository userRepository, IPasswordHasher passwordHasher)
    {
        _userIdentityProvider = userIdentityProvider;
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
    }

    public async Task<Result> Handle(CreateEmployeeCommand command, CancellationToken cancellationToken)
    {
        // TODO: A smarter why to do this check.
        // Solution?: Validate enum attribute with Fluent Validation.
        
        // Question: Is fine to verify the claim from JWT wihtout any databse integration ?
        
        if (_userIdentityProvider.UserPermission != UserPermission.Admin)
        {
            return Result.Failure(DomainErrors.General.ForbiddenOperationError);
        }
        
        var adminResult = await _userRepository.FindByIdAsync(_userIdentityProvider.UserId);
        
        if (adminResult.IsFailure)
        {
            return adminResult;
        }

        var newPasswordResult = _passwordHasher.HashPassword("abcda");
        if (newPasswordResult.IsFailure)
        {
            return newPasswordResult;
        }
        
        var newEmployeeResult = User.Create(command.Cnp, newPasswordResult.Value, command.Email, command.PhoneNumber, command.UserPermission);

        if (newEmployeeResult.IsFailure)
        {
            return newEmployeeResult;
        }

        var userWithSameCnp = await _userRepository.GetUserByCnp(newEmployeeResult.Value.Cnp);

        if (userWithSameCnp.IsSuccess)
        {
            return Result.Failure(DomainErrors.User.SameCnpError);
        }

        await _userRepository.AddAsync(newEmployeeResult.Value);

        return Result.Success();
    }
}