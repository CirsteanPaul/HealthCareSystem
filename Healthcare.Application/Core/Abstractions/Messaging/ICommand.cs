using Healthcare.Domain.Shared.Results;
using MediatR;

namespace Healthcare.Application.Core.Abstractions.Messaging;

public interface ICommand : IRequest<Result>
{
    
}

public interface ICommand<TResponse> : IRequest<Result<TResponse>>
{
    
}