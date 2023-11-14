using System.ComponentModel.DataAnnotations;
using Healthcare.Domain.Errors;
using Healthcare.Domain.Shared.Results;

namespace Healthcare.Domain.ValueObjects;

public record Email
{
    public const int MaxLength = 30;
    public string Value { get; private set; } = string.Empty;
    private Email(string email)
    {
        Value = email;
    }

    private Email() { }

    public static Result<Email> Create(string email)
    {
        var isEmail = new EmailAddressAttribute().IsValid(email);
        if (isEmail is false)
        {
            return Result.Failure<Email>(DomainErrors.User.EmailInvalidError);
        }

        if (email.Length > MaxLength)
        {
            return Result.Failure<Email>(DomainErrors.User.EmailTooLongError);
        }

        return new Email(email);
    }
}