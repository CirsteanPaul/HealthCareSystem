using DevOne.Security.Cryptography.BCrypt;
using Healthcare.Application.Core.Abstractions.Authentication;
using Healthcare.Application.Core.Abstractions.Email;
using Healthcare.Application.Test.Commands;
using Healthcare.Application.Test.Queries;
using Healthcare.Domain.Shared.Results;
using Healthcare.Infrastructure.Emails;
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
    private readonly IEmailSmtp _emailSmtp;
    public TestController(ISender sender, IMapper mapper, IJwtProvider jwtProvider, IUserIdentityProvider identityProvider, IEmailSmtp emailSmtp) : base(sender)
    {
        _mapper = mapper;
        _jwtProvider = jwtProvider;
        _identityProvider = identityProvider;
        _emailSmtp = emailSmtp;
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
    
    [HttpGet("username")]
    public async Task<IActionResult> GetUsername(string token)
    {
        return Ok(BCryptHelper.GenerateSalt());
        return Ok(_identityProvider.UserId);
    }

    [AllowAnonymous]
    [HttpPost("email")]
    public async Task<IActionResult> SendEmail()
    {
        var email = new MailRequest("paul3ioan@gmail.com", "Test123", "Mesaj important");
        await _emailSmtp.SendEmailAsync(email);

        return Ok();
    }
}