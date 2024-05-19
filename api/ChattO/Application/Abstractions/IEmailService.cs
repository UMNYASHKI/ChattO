using Application.DTOs.EmailData;
using Application.Helpers;

namespace Application.Abstractions;

public interface IEmailService
{
    Task<Result<bool>> SendEmailOnUserRegistration(UserRegistration userData);
    Task<Result<bool>> SendEmailOnUserRegistration(List<UserRegistration> usersData);
}
