namespace Healthcare.Domain.Shared.Results;

public sealed class ValidationResult<TValue> : Result<TValue>, IValidationResult
{
    private ValidationResult(Error[] errors)
        : base(default, false, IValidationResult.ValidationError) => Errors = errors;

    public Error[] Errors { get; }
    public static ValidationResult<TValue> WithErrors(Error[] errors) => new(errors);
    public static Result<TValue> AggregateValidationResults(params Result[] results)
    {
        var errorsList = new List<Error>();
        
        foreach (Result result in results)
        {
            if (result.IsFailure)
                errorsList.Add(result.Error);
        }

        if (errorsList.Any())
        {
            return WithErrors(errorsList.ToArray());
        }

        return Success<TValue>(default);
    }
}