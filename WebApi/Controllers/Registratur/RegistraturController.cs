using Healthcare.Application.Features.Registratur.CreateUser;
using Healthcare.Domain.Entities;
using Healthcare.Domain.Shared.Results;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebApi.Contracts;
using WebApi.Infrastructure;

namespace WebApi.Controllers.Registratur;

[ShouldHaveRole(Role = nameof(UserPermission.Registratur))]
public class RegistraturController : ApiController
{
    private readonly IMapper _mapper;
    
    public RegistraturController(ISender sender, IMapper mapper) : base(sender)
    {
        _mapper = mapper;
    }

    [Route(ApiRoutes.Registratur.CreateUser)]
    [HttpPost]
    public async Task<IActionResult> CreatePatient(CreateUserRequest request)
    {
        var command = _mapper.Map<CreateUserCommand>(request);

        var result = await Sender.Send(command);

        return result.Match<CreateUserResponse, IActionResult>(Created, HandleFailure);
    }
}