using Healthcare.Application.Core.Abstractions.Authentication;
using Healthcare.Application.Test.Commands;
using Healthcare.Application.Test.Queries;
using Healthcare.Domain.Shared;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Contracts;
using WebApi.Infrastructure;

namespace WebApi.Controllers;

public class TestController : ApiController
{
    private readonly IMapper _mapper;
    private readonly IJwtProvider _jwtProvider;
    private readonly IUserIdentityProvider _identityProvider;
    public TestController(ISender sender, IMapper mapper, IJwtProvider jwtProvider, IUserIdentityProvider identityProvider) : base(sender)
    {
        _mapper = mapper;
        _jwtProvider = jwtProvider;
        _identityProvider = identityProvider;
    }

    [AllowAnonymous]
    [HttpPost(ApiRoutes.Test.Create)]
    public async Task<IActionResult> CreateTest(CreateTestRequest request)
    {
        return (await Sender.Send(_mapper.Map<CreateTestRequestCommand>(request)))
            .Match<CreateTestResponse, IActionResult>(Ok, HandleFailure);
    }

    [AllowAnonymous]
    [HttpGet(ApiRoutes.Test.Get)]
    public async Task<IActionResult> GetTests()
    {
        var query = new GetTestRequestQuery();
        
        var result = await Sender.Send(query);

        return result.Match<GetTestResponse, IActionResult>(Ok, HandleFailure);
    }

    [AllowAnonymous]
    [HttpGet("token")]
    public async Task<IActionResult> Login(string username)
    {
        var token = _jwtProvider.Create(username);
        
        return Ok(token);
    }
    
    [HttpGet("username")]
    public async Task<IActionResult> GetUsername(string token)
    {
        return Ok(_identityProvider.UserId);
    }
}