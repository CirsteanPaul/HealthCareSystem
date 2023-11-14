using Healthcare.Domain.Shared;

namespace Healthcare.Domain.Errors;

public static class DomainErrors
{
    public static class Test
    {
        public static Error SomethingWentWrong => new($"Test.{nameof(SomethingWentWrong)}", "Something went wrong");
    }

    public static class General
    {
        public const string ForbiddenCode = $"General.{nameof(ForbiddenOperationError)}";
        
        public static Error InternalServerError =
            new($"General.{nameof(InternalServerError)}", "Please contact administrator.");

        public static Error ForbiddenOperationError = new(ForbiddenCode, "Forbidden.");

        public static Error EntityNotFoundError =
            new($"General.{nameof(EntityNotFoundError)}", "Entity not found.");

        public static Error PasswordCannotBeHashedError = new($"General.{nameof(PasswordCannotBeHashedError)}",
            "Password could not be hashed.");
    }

    public static class User
    {
        public static Error EmailInvalidError = new($"User.{nameof(EmailInvalidError)}", "Email is invalid.");

        public static Error PhoneNumberInvalidError =
            new($"User.{nameof(PhoneNumberInvalidError)}", "Phone number is invalid.");

        public static Error EmailTooLongError = new($"User.{nameof(EmailTooLongError)}", "Email is too long.");
        public static Error CnpInvalidError = new($"User.{nameof(CnpInvalidError)}", "CNP is invalid.");
        public static Error CnpLengthError = new($"User.{nameof(CnpLengthError)}", "CNP should have 13 characters.");
        
        public static Error SameCnpError = new($"User.{nameof(SameCnpError)}", "A user with the same CNP exists.");
        public static Error SamePasswordError = new($"User.{nameof(SamePasswordError)}", "New password is the same as the old one.");
        public static Error PasswordMatchError = new($"User.{nameof(PasswordMatchError)}", "Password is incorrect.");

        public static Error UserPermissionError =
            new($"User.{nameof(UserPermissionError)}", "User permission is unknown");
    }
}