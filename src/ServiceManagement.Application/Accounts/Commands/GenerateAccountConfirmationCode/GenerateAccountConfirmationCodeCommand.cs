namespace ServiceManagement.Application.Accounts.Commands.GenerateAccountConfirmationCode;

public record GenerateAccountConfirmationCodeCommand(string email) : IRequest<Result>;
