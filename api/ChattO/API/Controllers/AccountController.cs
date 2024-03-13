using API.DTOs.Requests.Account;
using Application.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class AccountController : BaseController
{
    private readonly IUserService _userService;

    public AccountController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost("Login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest) 
    {
        var loginResult = await _userService.AuthenticateUserAsync(loginRequest.Username, loginRequest.Password);

        return HandleResult(loginResult);
    }

    [Authorize]
    [HttpGet("Logout")]
    public async Task<IActionResult> Logout() 
    {
        await _userService.SignOutAsync();

        return Ok();
    }
}
