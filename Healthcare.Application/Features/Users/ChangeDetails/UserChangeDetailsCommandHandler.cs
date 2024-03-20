using Healthcare.Application.Core.Abstractions.Authentication;
using Healthcare.Application.Core.Abstractions.Data;
using Healthcare.Application.Core.Abstractions.Messaging;
using Healthcare.Domain.Shared.Results;

namespace Healthcare.Application.Features.Users.ChangeDetails;

public sealed class UserChangeDetailsCommandHandler : ICommandHandler<UserChangeDetailsCommand>
{
    private readonly IUserRepository _userRepository;
    private readonly IUserIdentityProvider _identityProvider;

    public UserChangeDetailsCommandHandler(IUserRepository userRepository, IUserIdentityProvider identityProvider)
    {
        _userRepository = userRepository;
        _identityProvider = identityProvider;
    }

    public async Task<Result> Handle(UserChangeDetailsCommand command, CancellationToken cancellationToken)
    {
        var userResult = await _userRepository.FindByIdAsync(_identityProvider.UserId);

        if (userResult.IsFailure)
        {
            return userResult;
        }
        
        userResult.Value.ChangeDetails(command.Email, command.PhoneNumber);

        await _userRepository.UpdateAsync(userResult.Value);
        
        return Result.Success();
    }
}