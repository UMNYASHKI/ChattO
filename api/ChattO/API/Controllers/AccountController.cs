using API.DTOs.Requests.Account;
using API.DTOs.Responses.Account;
using API.DTOs.Responses.User;
using Application.Abstractions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Google;
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
        var loginResult = await _userService.AuthenticateUserAsync(loginRequest.Email, loginRequest.Password);
        if (!loginResult.IsSuccessful)
            return HandleResult(loginResult);

        return Ok(new LoginResponse() { Token = loginResult.Data });
    }

    [HttpGet("GoogleLogin")]
    [ProducesResponseType<string>(StatusCodes.Status200OK)]
    [ProducesResponseType<string>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GoogleLogin()
    {
        var properties = new AuthenticationProperties
        {
            RedirectUri = Url.Action(nameof(GoogleCallBack)),
            Items =
            {
                { "scheme", GoogleDefaults.AuthenticationScheme }
            }
        };

        return Challenge(properties, GoogleDefaults.AuthenticationScheme);
    }

    [HttpGet("signin-google")]
    [ProducesResponseType<string>(StatusCodes.Status200OK)]
    [ProducesResponseType<string>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GoogleCallBack(string remoteError = null)
    {
        if (remoteError != null)
            return BadRequest(remoteError);

        return HandleResult(await _userService.AuthenticateUserByGoogleAsync());
    }

    [Authorize]
    [HttpGet("Logout")]
    [ProducesResponseType<string>(StatusCodes.Status200OK)]
    public async Task<IActionResult> Logout() 
    {
        await _userService.SignOutAsync();

        return Ok();
    }

    [Authorize]
    [HttpGet]
    [ProducesResponseType<UserDetailsResponse>(StatusCodes.Status200OK)]
    [ProducesResponseType<string>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAccountDetails()
    {
        var userResult = await _userService.GetCurrentUser();
        if (!userResult.IsSuccessful)
            return HandleResult(userResult);

        return Ok(Mapper.Map<UserDetailsResponse>(userResult.Data));
    }
}
