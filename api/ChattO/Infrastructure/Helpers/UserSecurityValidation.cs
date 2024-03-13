using Domain.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Infrastructure.Helpers;

public class UserSecurityValidation : JwtBearerEvents
{
    public async override Task TokenValidated(TokenValidatedContext context)
    {
        var userManager = context?.HttpContext.RequestServices.GetRequiredService<UserManager<AppUser>>();
        var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

        if (token == null)
        {
            context.Fail("No token provided");
        }

        var handler = new JwtSecurityTokenHandler();
        var jsonToken = (JwtSecurityToken)handler.ReadToken(token);
        var userId = jsonToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
        var secStamp = jsonToken.Claims.FirstOrDefault(c => c.Type == "SecurityStamp")?.Value;
        var user = await userManager.FindByIdAsync(userId);
        if (user?.SecurityStamp != secStamp)
        {
            context.Fail("User didn't pass security check");
        }

        return;
    }
}
