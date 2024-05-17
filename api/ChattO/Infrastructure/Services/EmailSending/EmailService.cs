using Application.Abstractions;
using Application.DTOs.EmailData;
using Application.Helpers;
using Infrastructure.Helpers;
using Mailjet.Client;
using Mailjet.Client.Resources;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using System.Linq;

namespace Infrastructure.Services.EmailSending;

public class EmailService : IEmailService 
{
    private readonly IMailjetClient _mailjetClient;
    private readonly EmailSettings _emailSettings;
    public EmailService(IMailjetClient mailjetClient, IOptions<EmailSettings> options)
    {
        _mailjetClient = mailjetClient;
        _emailSettings = options.Value;
    }

    public async Task<Result<bool>> SendEmailOnUserRegistration(UserRegistration userData)
    {
        MailjetRequest request = GetRequest(EmailTemplate.RegistrationId)
            .Property(Send.Vars, GetUserVars(userData));

        var response = await _mailjetClient.PostAsync(request);
        if (!response.IsSuccessStatusCode)
        {
            return Result.Failure<bool>("Failed to send email");
        }

        return Result.Success<bool>();
    }

    public async Task<Result<bool>> SendEmailOnUserRegistration(List<UserRegistration> usersData)
    {
        MailjetRequest request = GetRequest(EmailTemplate.RegistrationId)
        .Property(Send.Recipients, GetUsersDataWithVars(usersData));

        var response = await _mailjetClient.PostAsync(request);
        if (!response.IsSuccessStatusCode)
        {
            return Result.Failure<bool>("Failed to send emails");
        }

        return Result.Success<bool>();
    }

    private JArray GetUsersDataWithVars(List<UserRegistration> usersData)
    {
        return new JArray(usersData.Select(userData => new JObject
        {
            {"Email", userData.Email},
            {"Name", userData.DisplayName},
            {"Vars", GetUserVars(userData)
            }
        }));
    }

    private JObject GetUserVars(UserRegistration userData)
    {
        return new JObject
        {
            {"password", userData.GenaratedPassword},
            {"username", userData.DisplayName},
            {"role", userData.Role.ToString() },
            {"email", userData.Email}
        };
    }

    private MailjetRequest GetRequest(int templateId)
    {
        return new MailjetRequest
        {
            Resource = Send.Resource,
        }
        .Property(Send.FromEmail, _emailSettings.FromEmail)
        .Property(Send.FromName, _emailSettings.FromName)
        .Property(Send.MjTemplateID, templateId)
        .Property(Send.MjTemplateLanguage, true);
    }
}
