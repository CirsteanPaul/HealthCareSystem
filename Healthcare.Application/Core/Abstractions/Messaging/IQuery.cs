using Healthcare.Domain.Shared;
using MediatR;

namespace Healthcare.Application.Core.Abstractions.Messaging;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>
{
}