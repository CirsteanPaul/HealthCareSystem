using Healthcare.Domain.Shared;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebApi.Contracts;

namespace WebApi.Infrastructure;

[ApiController]
public class ApiController : ControllerBase
{
    protected readonly ISender Sender;

    protected ApiController(ISender sender)
    {
        Sender = sender;
    }
    
    protected IActionResult BadRequest(Error error) => BadRequest(new ApiErrorResponse(new[] { error }));
}