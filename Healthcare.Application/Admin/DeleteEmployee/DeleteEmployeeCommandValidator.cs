using FluentValidation;

namespace Healthcare.Application.Admin.DeleteEmployee;

public sealed class DeleteEmployeeCommandValidator : AbstractValidator<DeleteEmployeeCommand>
{
    public DeleteEmployeeCommandValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty();
    }
}