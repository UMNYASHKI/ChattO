using Application.Abstractions;
using Application.DTOs.EmailData;
using Application.Helpers;
using Domain.Models;
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
    private readonly IRepository<AppUser> _repository;
    public EmailService(IMailjetClient mailjetClient, IOptions<EmailSettings> options, IRepository<AppUser> repository)
    {
        _mailjetClient = mailjetClient;
        _emailSettings = options.Value;
        _repository = repository;
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

        await UpdateIsSentStatus(GetSuccessfullySentEmails(response));

        return Result.Success<bool>();
    }

    public async Task<Result<bool>> SendEmailOnUserRegistration(List<UserRegistration> usersData)
    {
        MailjetRequest request = GetRequest(EmailTemplate.RegistrationId)
        .Property(Send.Recipients, GetUsersDataWithVars(usersData));

        var response = await _mailjetClient.PostAsync(request);
        if (!response.IsSuccessStatusCode)
        {
            return Result.Failure<bool>(response.GetErrorMessage());
        }        

        await UpdateIsSentStatus(GetSuccessfullySentEmails(response));

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

    private List<string> GetSuccessfullySentEmails(MailjetResponse response)
    {
        return response.GetData()
            .Children()
            .Select(d => d["Email"].ToString())
            .ToList();
    }

    private async Task<Result<bool>> UpdateIsSentStatus(List<string> emails)
    {
        var usersResult = await _repository.GetAllAsync(u => emails.Contains(u.Email));
        if (!usersResult.IsSuccessful)
            return Result.Failure<bool>("Failed to get users");

        var users = usersResult.Data.ToList();
        users.ForEach(u => u.IsEmailSent = true);

        var tasks = new List<Task<Result<bool>>>();
        users.ForEach(u => tasks.Add(_repository.UpdateItemAsync(u)));
        var results = await Task.WhenAll(tasks);

        return Result.Success(true);
    }
}
