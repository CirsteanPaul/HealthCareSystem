using Healthcare.Application.Test.Commands;
using Healthcare.Application.Test.Queries;
using Healthcare.Domain.Shared;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebApi.Contracts;

namespace WebApi.Controllers;

public class TestController : ApiController
{
    public TestController(ISender sender) : base(sender)
    {
    }

    [HttpPost(ApiRoutes.Test.Create)]
    public async Task<IActionResult> CreateTest()
    {
        var command = new CreateTestRequestCommand("lala", 100);

        var result = await Sender.Send(command);

        return result.Match(Ok, BadRequest);
    }

    [HttpGet(ApiRoutes.Test.Get)]
    public async Task<IActionResult> GetTests()
    {
        var query = new GetTestRequestQuery();

        var result = await Sender.Send(query);

        return result.Match(Ok, BadRequest);
    }
}