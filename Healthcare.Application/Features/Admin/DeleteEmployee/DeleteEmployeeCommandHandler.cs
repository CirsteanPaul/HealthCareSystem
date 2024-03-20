using Healthcare.Application.Core.Abstractions.Authentication;
using Healthcare.Application.Core.Abstractions.Data;
using Healthcare.Application.Core.Abstractions.Messaging;
using Healthcare.Domain.Entities;
using Healthcare.Domain.Errors;
using Healthcare.Domain.Shared.Results;

namespace Healthcare.Application.Features.Admin.DeleteEmployee;

public sealed class DeleteEmployeeCommandHandler : ICommandHandler<DeleteEmployeeCommand>
{
    private readonly IUserRepository _userRepository;

    public DeleteEmployeeCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<Result> Handle(DeleteEmployeeCommand command, CancellationToken cancellationToken)
    {
        var userToBeDeletedResult = await _userRepository.FindByIdAsync(command.UserId);

        if (userToBeDeletedResult.IsFailure)
        {
            return userToBeDeletedResult;
        }

        await _userRepository.DeleteAsync(userToBeDeletedResult.Value.Id);
        
        return Result.Success();
    }
}