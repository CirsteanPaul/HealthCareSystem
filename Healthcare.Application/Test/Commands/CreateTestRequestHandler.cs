using Healthcare.Application.Core.Abstractions.Messaging;
using Healthcare.Domain.Shared;

namespace Healthcare.Application.Test.Commands;

public sealed class CreateTestRequestHandler : ICommandHandler<CreateTestRequestCommand>
{
    public CreateTestRequestHandler()
    {
    }

    public async Task<Result> Handle(CreateTestRequestCommand command, CancellationToken cancellationToken)
    {
        var newTest = new Domain.Test(Guid.NewGuid(), command.Name, command.Code);

        await Task.Delay(100);

        return Result.Success(new CreateTestResponse
        {
            Id = newTest.Id,
            Name = newTest.Name,
            Code = newTest.Code
        });
    }
}