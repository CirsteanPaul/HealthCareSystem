using Healthcare.Application.Admin.CreateEmployee;
using Healthcare.Application.Admin.DeleteEmployee;
using Healthcare.Domain.Shared.Results;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebApi.Contracts;
using WebApi.Infrastructure;

namespace WebApi.Controllers.Admin;

public class AdminController : ApiController
{
    private readonly IMapper _mapper;

    public AdminController(ISender sender, IMapper mapper) : base(sender)
    {
        _mapper = mapper;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
    [Route(ApiRoutes.Admin.CreateEmployee)]
    public async Task<IActionResult> CreateEmployee(CreateEmployeeRequest request)
    {
        var command = _mapper.Map<CreateEmployeeCommand>(request);

        var result = await Sender.Send(command);
        
        return result.Match(Created, HandleFailure);
    }
    
    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
    [Route(ApiRoutes.Admin.DeleteEmployee)]
    public async Task<IActionResult> DeleteEmployee(DeleteEmployeeRequest request)
    {
        var command = _mapper.Map<DeleteEmployeeCommand>(request);

        var result = await Sender.Send(command);
        
        return result.Match(Ok, HandleFailure);
    }
}