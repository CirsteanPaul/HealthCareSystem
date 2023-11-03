using Healthcare.Domain.Shared;

namespace WebApi.Contracts;

public class ApiErrorResponse
{
        public ApiErrorResponse(IReadOnlyCollection<Error> errors) => Errors = errors;
        public IReadOnlyCollection<Error> Errors { get; }
}