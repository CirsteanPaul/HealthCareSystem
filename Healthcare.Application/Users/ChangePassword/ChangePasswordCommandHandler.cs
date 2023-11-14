using Healthcare.Application.Core.Abstractions.Authentication;
using Healthcare.Application.Core.Abstractions.Cryptography;
using Healthcare.Application.Core.Abstractions.Data;
using Healthcare.Application.Core.Abstractions.Messaging;
using Healthcare.Domain.Errors;
using Healthcare.Domain.Shared.Results;

namespace Healthcare.Application.Users.ChangePassword;

public sealed class ChangePasswordCommandHandler : ICommandHandler<ChangePasswordCommand>
{
    private readonly IUserIdentityProvider _identityProvider;
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;

    public ChangePasswordCommandHandler(IUserIdentityProvider identityProvider, IUserRepository userRepository, IPasswordHasher passwordHasher)
    {
        _identityProvider = identityProvider;
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
    }

    public async Task<Result> Handle(ChangePasswordCommand command, CancellationToken cancellationToken)
    {
        if (command.UserId != _identityProvider.UserId)
        {
            return Result.Failure(DomainErrors.General.ForbiddenOperationError);
        }

        var userResult = await _userRepository.FindByIdAsync(command.UserId);

        if (userResult.IsFailure)
        {
            return userResult;
        }

        var hashedPasswordResult = _passwordHasher.HashPassword(command.NewPassword);

        if (hashedPasswordResult.IsFailure)
        {
            return hashedPasswordResult;
        }

        var isSamePassword = userResult.Value.VerifyPassword(hashedPasswordResult.Value);

        if (isSamePassword)
        {
            return Result.Failure(DomainErrors.User.SamePasswordError);
        }
        
        userResult.Value.ChangePassword(hashedPasswordResult.Value);

        await _userRepository.UpdateAsync(userResult.Value);

        return Result.Success();
    }
}