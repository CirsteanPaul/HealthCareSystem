using Healthcare.Application.Test.Commands;
using Healthcare.Application.Test.Queries;
using Healthcare.Domain.Shared;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebApi.Contracts;
using WebApi.Infrastructure;

namespace WebApi.Controllers;

public class TestController : ApiController
{
    private readonly IMapper _mapper;
    public TestController(ISender sender, IMapper mapper) : base(sender)
    {
        _mapper = mapper;
    }

    [HttpPost(ApiRoutes.Test.Create)]
    public async Task<IActionResult> CreateTest(CreateTestRequest request)
    {
        return (await Sender.Send(_mapper.Map<CreateTestRequestCommand>(request)))
            .Match<CreateTestResponse, IActionResult>(Ok, HandleFailure);
    }

    [HttpGet(ApiRoutes.Test.Get)]
    public async Task<IActionResult> GetTests()
    {
        var query = new GetTestRequestQuery();
        
        var result = await Sender.Send(query);

        return result.Match<GetTestResponse, IActionResult>(Ok, HandleFailure);
    }
}