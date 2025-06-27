using ServiceManagement.Application.Interfaces;

namespace ServiceManagement.Infrastructure.Implementations;

public class EmailSender : ICustomEmailSender
{
    public Task SendEmailAsync(string text)
    {
        // Implementation for sending an email
        Console.WriteLine(text);
        return Task.CompletedTask;
    }
}