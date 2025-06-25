using MediatR;
using ServiceManagement.Domain.Entities;

namespace ServiceManagement.Application.CompanyApplication.Commands.CreateCompany;

public record CreateCompanyCommand(string companyName,
    string phoneNumber,
    string street,
    string number,
    string complement,
    string neighborhood,
    string state,
    string city,
    string zipCode,
    string email,
    string password,
    string userName) : IRequest<Result>;