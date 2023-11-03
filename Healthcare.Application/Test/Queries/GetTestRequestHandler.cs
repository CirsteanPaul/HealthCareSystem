using Healthcare.Application.Core.Abstractions.Data;
using Healthcare.Application.Core.Abstractions.Messaging;
using Healthcare.Domain.Shared;

namespace Healthcare.Application.Test.Queries;

public class GetTestRequestHandler : IQueryHandler<GetTestRequestQuery, GetTestResponse>
{
    private readonly ITestRepository _repository;

    public GetTestRequestHandler(ITestRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<Result<GetTestResponse>> Handle(GetTestRequestQuery request, CancellationToken cancellationToken)
    {
        var tests = await _repository.GetAll(cancellationToken);

        var response = new GetTestResponse()
        {
            Tests = tests.Select(t => new TestDto(t.Id, t.Name, t.Code)).ToList()
        };

        return Result.Success(response);
    }
}