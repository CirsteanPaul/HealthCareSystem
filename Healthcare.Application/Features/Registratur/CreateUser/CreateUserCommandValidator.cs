using FluentValidation;

namespace Healthcare.Application.Features.Registratur.CreateUser;

public sealed class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(x => x.Cnp)
            .NotEmpty().Length(13);
        RuleFor(x => x.Email).EmailAddress();
        RuleFor(x => x.PhoneNumber).NotEmpty();
    }
}