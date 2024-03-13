using Application.Abstractions;
using Application.Helpers;
using Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Services.Authentication;

public class UserService : IUserService
{
    private readonly UserManager<AppUser> _userManager;

    private readonly SignInManager<AppUser> _signInManager;

    private readonly ITokenService _jwtService;

    private readonly IHttpContextAccessor _contextAccessor;

    public UserService(UserManager<AppUser> userManager, ITokenService jwtService, SignInManager<AppUser> signInManager, IHttpContextAccessor contextAccessor)
    {
        _userManager = userManager;
        _jwtService = jwtService;
        _signInManager = signInManager;
        _contextAccessor = contextAccessor;
    }

    public async Task<Result<bool>> RegisterUserAsync(AppUser user)
    { 
        var registerResult = await _userManager.CreateAsync(user, user.PasswordHash);
        if (!registerResult.Succeeded)
        {
            return Result.Failure<bool>("Failed to create user:\n" + string.Join('\n', registerResult.Errors));
        }

        var addToRoleResult = await _userManager.AddToRoleAsync(user, user.Role.ToString());
        if (!addToRoleResult.Succeeded)
        {
            return Result.Failure<bool>("Failed to give user role:\n" + string.Join('\n', registerResult.Errors));
        }

        return Result.Success<bool>(true);
    }

    public async Task<Result<string>> AuthenticateUserAsync(string username, string password)
    {
        var user = await _userManager.FindByNameAsync(username);
        const string errorMesage = "Invalid credentials";
        if (user == null)
        {
            return Result.Failure<string>(errorMesage);
        }

        if (!await _userManager.CheckPasswordAsync(user, password))
        {
            return Result.Failure<string>(errorMesage);
        }

        await UpdateUsersSecurityStamp(user, Guid.NewGuid().ToString());
        await _signInManager.SignInAsync(user, true);

        var jwt = _jwtService.GenerateToken(user);

        return Result.Success(jwt);
    }

    public async Task SignOutAsync()
    {
        var principal = _contextAccessor?.HttpContext?.User;
        var user = await _userManager.GetUserAsync(principal);
        await UpdateUsersSecurityStamp(user, "");
        await _signInManager.SignOutAsync();
    }

    private async Task UpdateUsersSecurityStamp(AppUser user, string securityStamp)
    {
        user.SecurityStamp = securityStamp;
        await _userManager.UpdateAsync(user);
    }
}
