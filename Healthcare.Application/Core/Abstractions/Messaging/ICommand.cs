using Healthcare.Domain.Shared;
using MediatR;

namespace Healthcare.Application.Core.Abstractions.Messaging;

public interface ICommand : IRequest<Result>
{
    
}

public interface ICommand<TResponse> : IRequest<Result<TResponse>>
{
    
}