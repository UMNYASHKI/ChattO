using API.Extensions;
using API.Helpers;
using API.Services.Abstractions;
using Application.Helpers;
using Domain.Enums;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace API.Services.Implementations;

public class UserService : IUserService
{
    private readonly UserManager<AppUser> _userManager;

    private readonly SignInManager<AppUser> _signInManager;

    private readonly IJwtService _jwtService;

    private readonly IHttpContextAccessor _contextAccessor;

    public UserService(UserManager<AppUser> userManager, IJwtService jwtService, SignInManager<AppUser> signInManager, IHttpContextAccessor contextAccessor)
    {
        _userManager = userManager;
        _jwtService = jwtService;
        _signInManager = signInManager;
        _contextAccessor = contextAccessor;
    }

    public async Task<Result<bool>> RegisterUserAsync(AppUser user)
    {
        var registerResult = await _userManager.CreateAsync(user);
        if (!registerResult.Succeeded)
        {
            return new Result<bool>(false, "Failed to create user:\n" + string.Join('\n', registerResult.Errors));
        }

        var addToRoleResult = await _userManager.AddToRoleAsync(user, user.Role.ToString());
        if (!addToRoleResult.Succeeded)
        {
            return new Result<bool>(false, "Failed to give user role:\n" + string.Join('\n', registerResult.Errors));
        }

        return new Result<bool>(true);
    }

    public async Task<Result<string>> AuthenticateUserAsync(string username, string password)
    {
        var user = await _userManager.FindByNameAsync(username);
        const string errorMesage = "Invalid credentials";
        if (user == null)
        {
            return new Result<string>(false, errorMesage);
        }

        if (!await _userManager.CheckPasswordAsync(user, password))
        {
            return new Result<string>(false, errorMesage);
        }

        await UpdateUsersSecurityStamp(user, Guid.NewGuid().ToString());
        await _signInManager.SignInAsync(user, true);

        var jwt = _jwtService.GenerateJwtToken(user);

        return new Result<string>(true, jwt);
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
