using Healthcare.Application.Core.Abstractions.Authentication;
using Healthcare.Application.Core.Abstractions.Data;
using Healthcare.Application.Core.Abstractions.Messaging;
using Healthcare.Domain.Entities;
using Healthcare.Domain.Errors;
using Healthcare.Domain.Shared.Results;

namespace Healthcare.Application.Admin.DeleteEmployee;

public sealed class DeleteEmployeeCommandHandler : ICommandHandler<DeleteEmployeeCommand>
{
    private readonly IUserIdentityProvider _identityProvider;
    private readonly IUserRepository _userRepository;

    public DeleteEmployeeCommandHandler(IUserIdentityProvider identityProvider, IUserRepository userRepository)
    {
        _identityProvider = identityProvider;
        _userRepository = userRepository;
    }

    public async Task<Result> Handle(DeleteEmployeeCommand command, CancellationToken cancellationToken)
    {
        if (_identityProvider.UserPermission != UserPermission.Admin)
        {
            return Result.Failure(DomainErrors.General.ForbiddenOperationError);
        }

        var userToBeDeletedResult = await _userRepository.FindByIdAsync(command.UserId);

        if (userToBeDeletedResult.IsFailure)
        {
            return userToBeDeletedResult;
        }

        await _userRepository.DeleteAsync(userToBeDeletedResult.Value.Id);
        
        return Result.Success();
    }
}