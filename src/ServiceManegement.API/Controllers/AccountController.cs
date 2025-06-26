using MediatR;
using Microsoft.AspNetCore.Mvc;
using ServiceManagement.Application.AccountApplication.Commands;

namespace ServiceManagement.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    public IActionResult CreateAccount([FromBody] CreateAccountCommand request)
    {
        var result = mediator.Send(request);

        if (!result.Result.IsSuccess)
        {
            return BadRequest(result.Result);
        }
        return Ok(result.Result);
    }
}
