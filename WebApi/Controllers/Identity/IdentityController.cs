using Healthcare.Application.Features.Users.Login;
using Healthcare.Domain.Shared.Results;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Contracts;
using WebApi.Infrastructure;

namespace WebApi.Controllers.Identity;

public sealed class IdentityController : ApiController
{    
    private readonly IMapper _mapper;
    
    public IdentityController(ISender sender, IMapper mapper) : base(sender)
    {
        _mapper = mapper;
    }
    
    [HttpPost]
    [Route(ApiRoutes.Identity.Login)]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(UserLoginResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
    public async Task<IActionResult> Login(UserLoginRequest request)
    {
        var command = _mapper.Map<UserLoginCommand>(request);

        var result = await Sender.Send(command);

        return result.Match<UserLoginResponse, IActionResult>(Created, HandleFailure);
    }
}