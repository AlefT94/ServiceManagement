using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceManagement.API.Dto;
using ServiceManagement.Application.Company.Commands.CreateCompany;

namespace ServiceManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController(IMediator mediator) : ControllerBase
    {
        [HttpPost]
        public IActionResult CreateCompany([FromBody] CreateCompanyDto request)
        {
            var result = mediator.Send(request);
            return Ok(result.Result);
        }
    }
}
