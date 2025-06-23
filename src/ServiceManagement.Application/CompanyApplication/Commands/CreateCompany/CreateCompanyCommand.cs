using MediatR;
using ServiceManagement.Domain.Entities;

namespace ServiceManagement.Application.CompanyApplication.Commands.CreateCompany;

public record CreateCompanyCommand(Company company) : IRequest<string>;
