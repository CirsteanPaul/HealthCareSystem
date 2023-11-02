using Healthcare.Domain.Shared;
using MediatR;

namespace Healthcare.Application.Core.Abstractions.Messaging;

public interface ICommandHandler<TCommand> : IRequestHandler<TCommand, Result>
    where TCommand : ICommand<Result>
{
    
}