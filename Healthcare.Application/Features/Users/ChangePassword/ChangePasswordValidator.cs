using FluentValidation;

namespace Healthcare.Application.Features.Users.ChangePassword;

public sealed class ChangePasswordValidator : AbstractValidator<ChangePasswordCommand>
{
    public ChangePasswordValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty();
        
        
        RuleFor(x => x.NewPassword)
            .NotEmpty().MinimumLength(7);
    }
}