using Healthcare.Domain.Shared.Results;
using MediatR;

namespace Healthcare.Application.Core.Abstractions.Messaging;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>
{
}