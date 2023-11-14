using Healthcare.Application.Core.Abstractions.Data;
using Healthcare.Application.Core.Abstractions.Messaging;
using Healthcare.Domain.Shared.Results;

namespace Healthcare.Application.Test.Commands;

public sealed class CreateTestRequestHandler : ICommandHandler<CreateTestRequestCommand, CreateTestResponse>
{
    private readonly ITestRepository _repository;
    public CreateTestRequestHandler(ITestRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<CreateTestResponse>> Handle(CreateTestRequestCommand command, CancellationToken cancellationToken)
    {
        var newTest = new Domain.Entities.Test(Guid.NewGuid(), command.Name, command.Code);

        var result = await _repository.AddAsync(newTest);

        if (result.IsSuccess)
        {
            return Result.Success<CreateTestResponse>(new CreateTestResponse()
            {
                Id = result.Value.Id,
                Name = result.Value.Name,
                Code = result.Value.Code
            });
        }
        return Result.Failure<CreateTestResponse>(result.Error);
    }
}