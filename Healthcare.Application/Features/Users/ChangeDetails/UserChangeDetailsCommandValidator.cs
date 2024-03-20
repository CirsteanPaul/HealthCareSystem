using FluentValidation;
using Healthcare.Domain.Errors;

namespace Healthcare.Application.Features.Users.ChangeDetails;

public sealed class UserChangeDetailsCommandValidator : AbstractValidator<UserChangeDetailsCommand>
{
    public UserChangeDetailsCommandValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty();

        RuleFor(x => x.Email)
            .EmailAddress().WithMessage(DomainErrors.User.EmailInvalidError.Message);
        
        RuleFor(x => x.PhoneNumber)
            .NotEmpty();
    }
}