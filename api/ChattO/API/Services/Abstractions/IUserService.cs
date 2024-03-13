using Application.Helpers;
using Domain.Models;

namespace API.Services.Abstractions
{
    public interface IUserService
    {
        Task<Result<bool>> RegisterUserAsync(AppUser user);

        Task<Result<string>> AuthenticateUserAsync(string username, string password);

        Task SignOutAsync();
    }
}
