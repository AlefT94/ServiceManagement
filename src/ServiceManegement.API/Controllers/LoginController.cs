using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceManagement.Application.LoginApplication.Queries;

namespace ServiceManagement.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LoginController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    public IActionResult Login([FromBody] LoginQuery request)
    {
        var result = mediator.Send(request);

        if (!result.Result.IsSuccess)
        {
            return Unauthorized(result.Result);
        }
        return Ok(result.Result);
    }

    [HttpGet]
    [Authorize]
    public IActionResult Get()
    {
        return Ok("Service Management API is running");
    }
}
