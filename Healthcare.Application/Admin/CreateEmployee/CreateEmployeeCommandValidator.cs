using FluentValidation;
using Healthcare.Domain.Errors;

namespace Healthcare.Application.Admin.CreateEmployee;

public sealed class CreateEmployeeCommandValidator : AbstractValidator<CreateEmployeeCommand>
{
    public CreateEmployeeCommandValidator()
    {
        RuleFor(x => x.Cnp)
            .NotEmpty().Length(13);
        
        RuleFor(x => x.UserPermission)
            .IsInEnum().WithMessage(DomainErrors.User.UserPermissionError.Message);
        
        RuleFor(x => x.PhoneNumber)
            .NotEmpty();
        
        RuleFor(x => x.Email)
            .NotEmpty();
        
        RuleFor(x => x.Email)
            .EmailAddress().WithMessage(DomainErrors.User.EmailInvalidError.Message);
    }
}