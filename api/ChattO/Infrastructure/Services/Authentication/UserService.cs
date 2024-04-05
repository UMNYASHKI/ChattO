using Application.Abstractions;
using Application.Helpers;
using Domain.Models;
using Infrastructure.Extensions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

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

    public async Task<Result<string>> AuthenticateUserAsync(string username, string password) // email??
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

    public async Task<Result<string>> AuthenticateUserByGoogleAsync()
    {
        var externalLogin = await _contextAccessor.HttpContext.AuthenticateAsync(GoogleDefaults.AuthenticationScheme);
        var signInResult = externalLogin.GetSigInData();

        var user = await _userManager.FindByEmailAsync(signInResult.Email);
        if (user == null)
            return Result.Failure<string>("Fail to find user by email");

        var loginUser = await _userManager.FindByLoginAsync(signInResult.Provider, signInResult.ProviderKey);

        if (loginUser is null)
        {
            UserLoginInfo login = new UserLoginInfo(signInResult.Provider, signInResult.ProviderKey, signInResult.Provider.ToUpper());
            var result = await _userManager.AddLoginAsync(user, login);

            if (!result.Succeeded)
                return Result.Failure<string>("Fail to add user log in");
        }

        await UpdateUsersSecurityStamp(user, Guid.NewGuid().ToString());
        await _signInManager.SignInAsync(user, true);

        var jwt = _jwtService.GenerateToken(user);

        return Result.Success(jwt);
    }

    public async Task SignOutAsync()
    {
        var userResult = await GetCurrentUser();

        await UpdateUsersSecurityStamp(userResult.Data, "");
        await _signInManager.SignOutAsync();
    }

    public async Task<Result<AppUser>> GetCurrentUser()
    {
        var principal = _contextAccessor?.HttpContext?.User;
        var user = await _userManager.GetUserAsync(principal);

        if (user is null)
            return Result.Failure<AppUser>("Fail to get current user");

        return Result.Success<AppUser>(user);
    } 

    private async Task UpdateUsersSecurityStamp(AppUser user, string securityStamp)
    {
        user.SecurityStamp = securityStamp;
        await _userManager.UpdateAsync(user);
    }
}
