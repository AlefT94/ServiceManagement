using MediatR;
using Microsoft.AspNetCore.Mvc;
using ServiceManagement.Application.AccountApplication.Commands.CreateAccount;
using ServiceManagement.Application.AccountApplication.Commands.GenerateAccountConfirmationCode;

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

    [HttpPost("GenerateConfirmationCode")]
    public IActionResult GenerateConfirmationCode([FromBody] GenerateAccountConfirmationCodeCommand request)
    {
        var result = mediator.Send(request);
        if (!result.Result.IsSuccess)
        {
            return BadRequest(result.Result);
        }
        return Ok();
    }
}
