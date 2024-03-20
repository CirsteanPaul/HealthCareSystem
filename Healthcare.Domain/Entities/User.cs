using Healthcare.Domain.Events;
using Healthcare.Domain.Shared.EntityTypes;
using Healthcare.Domain.Shared.Results;
using Healthcare.Domain.ValueObjects;

namespace Healthcare.Domain.Entities;

public enum UserPermission
{
    Admin,
    Doctor,
    Pharmacist,
    Registratur,
    Patient,
    Unknown = 1001
}

public class User : AggregateRoot
{
    private string _hashedPassword;
    public Cnp Cnp { get; private set; }
    public Email Email { get; private set; }
    public PhoneNumber PhoneNumber { get; private set; }
    public UserPermission UserPermission { get; private set; }

    private User()
    {
    }

    private User(Guid id, Cnp cnp, string hashedPassword, Email email, PhoneNumber phoneNumber,
        UserPermission userPermission) : base(id)
    {
        Cnp = cnp;
        _hashedPassword = hashedPassword;
        Email = email;
        PhoneNumber = phoneNumber;
        UserPermission = userPermission;
    }

    public static Result<User> Create(string cnp, string hashedPassword, string email, string phoneNumber,
        UserPermission userPermission)
    {
        var cnpResult = Cnp.Create(cnp);
        var emailResult = Email.Create(email);
        var phoneNumberResult = PhoneNumber.Create(phoneNumber);

        var validationResult = ValidationResult<User>.AggregateValidationResults(cnpResult, emailResult, phoneNumberResult);
        if (validationResult.IsFailure)
        {
            return validationResult;
        }

        var user = new User(
            Guid.NewGuid(),
            cnpResult.Value,
            hashedPassword,
            emailResult.Value,
            phoneNumberResult.Value,
            userPermission);

        user.AddDomainEvent(new CreatedUserDomainEvent(user));

        return user;
    }

    public bool VerifyPassword(string hashedPassword) => hashedPassword == _hashedPassword;

    public void ChangePassword(string hashedPassword)
    {
        _hashedPassword = hashedPassword;

        AddDomainEvent(new ChangedPasswordUserDomainEvent(this));
    }

    public void ChangeDetails(string email, string phoneNumber)
    {
        // TODO: We need email verification step.
        var emailResult = Email.Create(email);
        var phoneNumberResult = PhoneNumber.Create(phoneNumber);

        Email = emailResult.Value;
        PhoneNumber = phoneNumberResult.Value;

        AddDomainEvent(new ChangedDetailsUserDomainEvent(this));
    }
}