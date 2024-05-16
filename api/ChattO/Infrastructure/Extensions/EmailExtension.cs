using Application.Abstractions;
using Infrastructure.Services.EmailSending;
using Mailjet.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Extensions;

public static class EmailExtension
{
    public static void AddEmailSending(this IServiceCollection services, IConfiguration configuration)
    {
        var apiKey = configuration.GetSection("MailjetSettings:ApiId").Get<string>();
        var apiSecret = configuration.GetSection("MailjetSettings:ApiSecret").Get<string>();

        services.AddHttpClient<IMailjetClient, MailjetClient>(client =>
        {
            client.SetDefaultSettings();
            client.UseBasicAuthentication(apiKey, apiSecret);
        }); 

        services.AddScoped<IEmailService, EmailService>();
    }
}
