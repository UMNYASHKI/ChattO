using API.Helpers;
using Domain.Enums;
using Infrastructure.Abstractions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace API.Extensions;

public static class MigrationExtension
{
    public static async Task ApplyMigration(this IApplicationBuilder builder) 
    {
        var localScope = builder.ApplicationServices.CreateScope();

        var dbContext = localScope.ServiceProvider.GetRequiredService<IChattoDbContext>();
        var roleManager = localScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole<Guid>>>();

        dbContext.Database.Migrate();
        if (!await roleManager.RoleExistsAsync(AppUserRole.SystemAdmin.ToString())) 
        {
            var userRoles = Enum.GetValues<AppUserRole>();
            foreach (var userRole in userRoles) 
            {
                await roleManager.CreateAsync(new IdentityRole<Guid>() { Name = userRole.ToString() });
            }
        }
    }
}
