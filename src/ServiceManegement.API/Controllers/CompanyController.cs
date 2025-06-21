using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceManagement.Application.Company.Commands.CreateCompany;

namespace ServiceManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController(IMediator mediator) : ControllerBase
    {
        [HttpPost]
        public IActionResult CreateCompany([FromBody] CreateCompanyCommand company)
        {
            var result = mediator.Send(company);
            return Ok(result.Result);
        }
    }
}
