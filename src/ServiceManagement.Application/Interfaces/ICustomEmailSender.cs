namespace ServiceManagement.Application.Interfaces;

public interface ICustomEmailSender
{
    public Task SendEmailAsync(string text);
}
