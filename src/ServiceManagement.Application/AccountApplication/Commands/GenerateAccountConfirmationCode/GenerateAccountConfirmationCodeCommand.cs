namespace ServiceManagement.Application.AccountApplication.Commands.GenerateAccountConfirmationCode;

public record GenerateAccountConfirmationCodeCommand(string email) : IRequest<Result>;
