using FluentValidation;

namespace Healthcare.Application.Features.Admin.DeleteEmployee;

public sealed class DeleteEmployeeCommandValidator : AbstractValidator<DeleteEmployeeCommand>
{
    public DeleteEmployeeCommandValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty();
    }
}