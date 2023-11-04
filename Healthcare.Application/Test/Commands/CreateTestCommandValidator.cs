using FluentValidation;

namespace Healthcare.Application.Test.Commands;

public class CreateTestCommandValidator : AbstractValidator<CreateTestRequestCommand>
{
    public CreateTestCommandValidator()
    {
        RuleFor(x => x.Code).GreaterThan(0);

        RuleFor(x => x.Name).NotEmpty();
    }
}