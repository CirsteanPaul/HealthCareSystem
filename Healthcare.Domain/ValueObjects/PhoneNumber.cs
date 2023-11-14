using System.ComponentModel.DataAnnotations;
using Healthcare.Domain.Errors;
using Healthcare.Domain.Shared.Results;

namespace Healthcare.Domain.ValueObjects;

public record PhoneNumber
{
    public string Value { get; private set; } = string.Empty;
    
    private PhoneNumber() { }
    private PhoneNumber(string phoneNumber)
    {
        Value = phoneNumber;
    }

    public static Result<PhoneNumber> Create(string phoneNumber)
    {
        var isPhoneNumber = new PhoneAttribute().IsValid(phoneNumber);
        
        if (isPhoneNumber is false)
        {
            return Result.Failure<PhoneNumber>(DomainErrors.User.PhoneNumberInvalidError);
        }

        return new PhoneNumber(phoneNumber);
    }
}