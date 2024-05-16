using Application.Abstractions;
using Mailjet.Client;

namespace Infrastructure.Services.EmailSending;

public class EmailService : IEmailService 
{
    private readonly IMailjetClient _mailjetClient;
    public EmailService(IMailjetClient mailjetClient)
    {
        _mailjetClient = mailjetClient;
    }
}
