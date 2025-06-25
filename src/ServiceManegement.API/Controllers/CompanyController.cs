using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceManagement.Application.CompanyApplication.Commands.CreateCompany;

namespace ServiceManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController(IMediator mediator) : ControllerBase
    {
        [HttpPost]
        public IActionResult CreateCompany([FromBody] CreateCompanyCommand request)
        {
            var result = mediator.Send(request);
            
            if(!result.Result.IsSuccess)
            {
                return BadRequest(result.Result);
            }
            return Ok(result.Result);
        }
    }
}
