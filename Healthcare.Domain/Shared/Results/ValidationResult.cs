namespace Healthcare.Domain.Shared.Results;

public sealed class ValidationResult : Result, IValidationResult
{
    private ValidationResult(Error[] errors)
        : base(false, IValidationResult.ValidationError) => Errors = errors;

    public Error[] Errors { get; }
    public static ValidationResult WithErrors(Error[] errors) => new(errors);
    
    public static Result AggregateValidationResults(params Result[] results)
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

        return Success();
    }
}