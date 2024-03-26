using Infrastructure.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace API.Extensions;

public static class MigrationExtension
{
    public static void ApplyMigration(this IApplicationBuilder builder) 
    {
        var localScope = builder.ApplicationServices.CreateScope();

        var dbContext = localScope.ServiceProvider.GetRequiredService<IChattoDbContext>();

        dbContext.Database.Migrate();
    }
}
