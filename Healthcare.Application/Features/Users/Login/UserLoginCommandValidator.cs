using FluentValidation;

namespace Healthcare.Application.Features.Users.Login;

public sealed class UserLoginCommandValidator : AbstractValidator<UserLoginCommand>
{
    public UserLoginCommandValidator()
    {
        RuleFor(x => x.Cnp)
            .NotEmpty().Length(13);

        RuleFor(x => x.Password)
            .NotEmpty().MinimumLength(7);
    }
}