using API.DTOs.Requests.Account;
using API.Services.Implementations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class AccountController : BaseController
{
    private readonly UserService _userService;

    public AccountController(UserService userService)
    {
        _userService = userService;
    }

    [HttpPost("Log-in")]
    public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest) 
    {
        var loginResult = await _userService.AuthenticateUserAsync(loginRequest.UserName, loginRequest.Password);
        if (!loginResult.IsSuccessful)
        {
            return BadRequest(loginResult.Message);
        }

        return Ok(loginResult.Data);
    }

    [Authorize]
    [HttpGet("Log-out")]
    public async Task<IActionResult> Logout() 
    {
        await _userService.SignOutAsync();
        return Ok();
    }
}
