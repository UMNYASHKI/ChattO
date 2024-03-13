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

    [HttpPost("Login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest) 
    {
        var loginResult = await _userService.AuthenticateUserAsync(loginRequest.Username, loginRequest.Password);
        if (!loginResult.IsSuccessful)
        {
            return BadRequest(loginResult.Message);
        }

        return Ok(loginResult.Data);
    }

    [Authorize]
    [HttpGet("Logout")]
    public async Task<IActionResult> Logout() 
    {
        await _userService.SignOutAsync();
        return Ok();
    }
}
