using Domain.Models;
using Domain.Models.Files;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.DataAccess.EntityTypeConfigurations;

public class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
{
    public void Configure(EntityTypeBuilder<AppUser> builder)
    {
        builder.HasMany(user => user.Tickets)
            .WithOne(ticket=>ticket.AppUser)
            .HasForeignKey(ticket=>ticket.AppUserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(user=>user.ProfileImage)
            .WithOne(img=>img.AppUser)
            .HasForeignKey<ProfileImage>(img=>img.AppUserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(user=>user.AppUserGroups)
            .WithOne(appUserGroup=>appUserGroup.AppUser)
            .HasForeignKey(appUserGroup=>appUserGroup.AppUserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(user => user.AppUserFeeds)
            .WithOne(appUserGroup => appUserGroup.AppUser)
            .HasForeignKey(appUserGroup => appUserGroup.AppUserId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
