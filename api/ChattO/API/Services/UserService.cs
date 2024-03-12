using Domain.Enums;
using Domain.Models;
using Microsoft.AspNetCore.Identity;

namespace API.Services;

public class UserService
{
    private readonly UserManager<AppUser> _userManager;

    private readonly RoleManager<IdentityRole<Guid>> _roleManager;

    public UserService(UserManager<AppUser> userManager, RoleManager<IdentityRole<Guid>> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    //public async Task RegisterUserAsync(AppUser user, AppUserRole userRole) 
    //{
    //    var registerResult = await _userManager.CreateAsync(user);
    //    if (!registerResult.Succeeded) 
    //    {
    //        return new Result<bool>(registerResult.Errors.);
    //    }
    //}
}
