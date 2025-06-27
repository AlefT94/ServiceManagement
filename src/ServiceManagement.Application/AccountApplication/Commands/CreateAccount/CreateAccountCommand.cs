namespace ServiceManagement.Application.AccountApplication.Commands.CreateAccount;

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
    string userName,
    string validationCode) : IRequest<Result>;
