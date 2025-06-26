using MediatR;
using ServiceManagement.Domain.Entities;

namespace ServiceManagement.Application.AccountApplication.Commands;

public record CreateAccountCommand(string companyName,
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
