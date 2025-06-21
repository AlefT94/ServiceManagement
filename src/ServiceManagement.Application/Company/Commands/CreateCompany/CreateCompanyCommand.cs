using MediatR;

namespace ServiceManagement.Application.Company.Commands.CreateCompany;

public record CreateCompanyCommand (string CompanyName) :IRequest<string>;
