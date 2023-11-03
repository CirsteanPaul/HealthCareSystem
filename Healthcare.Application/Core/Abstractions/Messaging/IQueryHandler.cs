using Healthcare.Domain.Shared;
using MediatR;

namespace Healthcare.Application.Core.Abstractions.Messaging;

public interface IQueryHandler<TQuery, TResponse> : IRequestHandler<TQuery, Result<TResponse>>
    where TQuery : IQuery<TResponse>
{
    
}