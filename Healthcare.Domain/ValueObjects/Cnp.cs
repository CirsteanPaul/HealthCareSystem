using Healthcare.Domain.Errors;
using Healthcare.Domain.Shared.Results;

namespace Healthcare.Domain.ValueObjects;

public record Cnp
{
    public const int Length = 13;
    public string Value { get; private set; } = string.Empty;
    private Cnp(string email)
    {
        Value = email;
    }
    
    private Cnp() { }

    public static Result<Cnp> Create(string cnp)
    {
        if (string.IsNullOrWhiteSpace(cnp))
        {
            return Result.Failure<Cnp>(DomainErrors.User.CnpInvalidError);
        }

        if (cnp.Length != Length)
        {
            return Result.Failure<Cnp>(DomainErrors.User.CnpLengthError);
        }

        return new Cnp(cnp);
    }
}