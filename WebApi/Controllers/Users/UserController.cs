using Healthcare.Application.Features.Users.ChangeDetails;
using Healthcare.Application.Features.Users.ChangePassword;
using Healthcare.Domain.Entities;
using Healthcare.Domain.Shared.Results;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebApi.Contracts;
using WebApi.Infrastructure;

namespace WebApi.Controllers.Users;

[ShouldHaveRole(Role = nameof(UserPermission.Patient))]
public sealed class UserController : ApiController
{
    private readonly IMapper _mapper;
    
    public UserController(ISender sender, IMapper mapper) : base(sender)
    {
        _mapper = mapper;
    }
    
    [HttpPost]
    [Route(ApiRoutes.User.ChangePassword)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
    public async Task<IActionResult> ChangePassword(UserChangePasswordRequest request)
    {
        var command = _mapper.Map<ChangePasswordCommand>(request);

        var result = await Sender.Send(command);

        return result.Match(Ok, HandleFailure);
    }
    
    [HttpPut]
    [Route(ApiRoutes.User.ChangeDetails)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
    public async Task<IActionResult> ChangeDetails(UserChangeDetailsRequest request)
    {
        var command = _mapper.Map<UserChangeDetailsCommand>(request);

        var result = await Sender.Send(command);

        return result.Match(Ok, HandleFailure);
    }
}