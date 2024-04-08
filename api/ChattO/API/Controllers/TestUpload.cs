using Application.Files.Commands;
using Application.Files.Queries;
using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
public class TestController : BaseController
{ 
    private readonly UserManager<AppUser> _userManager;

    public TestController(UserManager<AppUser> userManager)
    {
        _userManager = userManager;
    }

    [HttpGet("Download")]
    public async Task<IActionResult> Index(Guid profileId)
    {
        var user = await _userManager.GetUserAsync(User);
        return HandleResult(await Mediator.Send(new DownloadProfileImage.Command() { AccountId = profileId, Domain = user.Organization.Domain }));
    }

    [HttpPost("Upload")]
    public async Task<IActionResult> Upload(IFormFile file) 
    {
        var user = await _userManager.GetUserAsync(User);
        return HandleResult(await Mediator.Send(new UploadProfileImage.Command() { AccountId = user.Id, Domain = user.Organization.Domain, File = file }));
    }
}
