using Healthcare.Application.Core.Abstractions.Authentication;
using Healthcare.Application.Core.Abstractions.Cryptography;
using Healthcare.Application.Core.Abstractions.Data;
using Healthcare.Application.Core.Abstractions.Messaging;
using Healthcare.Domain.Errors;
using Healthcare.Domain.Shared.Results;
using Healthcare.Domain.ValueObjects;

namespace Healthcare.Application.Users.Login;

public sealed class UserLoginCommandHandler : ICommandHandler<UserLoginCommand, UserLoginResponse>
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtProvider _jwtProvider;
    private readonly IPasswordHasher _passwordHasher;

    public UserLoginCommandHandler(IUserRepository userRepository, IJwtProvider jwtProvider, IPasswordHasher passwordHasher)
    {
        _userRepository = userRepository;
        _jwtProvider = jwtProvider;
        _passwordHasher = passwordHasher;
    }

    public async Task<Result<UserLoginResponse>> Handle(UserLoginCommand command, CancellationToken cancellationToken)
    {
        var cnpResult = Cnp.Create(command.Cnp);
        
        if (cnpResult.IsFailure)
        {
            return Result.Failure<UserLoginResponse>(cnpResult.Error);
        }
        
        var userResult = await _userRepository.GetUserByCnp(cnpResult.Value);

        if (userResult.IsFailure)
        {
            return Result.Failure<UserLoginResponse>(userResult.Error);
        }

        var user = userResult.Value;

        var hashPasswordResult = _passwordHasher.HashPassword(command.Password);

        if (hashPasswordResult.IsFailure)
        {
            return Result.Failure<UserLoginResponse>(hashPasswordResult.Error);
        }
        
        var isPasswordMatch = user.VerifyPassword(hashPasswordResult.Value);

        if (!isPasswordMatch)
        {
            return Result.Failure<UserLoginResponse>(DomainErrors.User.PasswordMatchError);
        }
        
        var accessToken = _jwtProvider.Create(user.Id, user.Email.Value, user.UserPermission);

        return new UserLoginResponse{ AccessToken = accessToken };
    }
}