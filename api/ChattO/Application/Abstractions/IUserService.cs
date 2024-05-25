using Application.Helpers;
using Domain.Models;

namespace Application.Abstractions
{
    public interface IUserService
    {
        Task<Result<bool>> RegisterUserAsync(AppUser user);

        Task<Result<string>> AuthenticateUserAsync(string username, string password);

        Task SignOutAsync();

        Task<Result<AppUser>> GetCurrentUser();

        Task<Result<string>> AuthenticateUserByGoogleAsync();
    }
}
