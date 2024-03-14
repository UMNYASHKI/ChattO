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
    [ProducesResponseType<string>(StatusCodes.Status200OK)]
    [ProducesResponseType<string>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest) 
    {
        var loginResult = await _userService.AuthenticateUserAsync(loginRequest.Username, loginRequest.Password);

        return HandleResult(loginResult);
    }

    [Authorize]
    [HttpGet("Logout")]
    [ProducesResponseType<string>(StatusCodes.Status200OK)]
    public async Task<IActionResult> Logout() 
    {
        await _userService.SignOutAsync();

        return Ok();
    }
}
