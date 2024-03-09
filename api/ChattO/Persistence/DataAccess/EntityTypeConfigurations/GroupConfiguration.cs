using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.DataAccess.EntityTypeConfigurations;

public class GroupConfiguration : IEntityTypeConfiguration<Group>
{
    public void Configure(EntityTypeBuilder<Group> builder)
    {
        builder.HasMany(group=>group.AppUserGroups)
            .WithOne(appUserGroup=>appUserGroup.Group)
            .HasForeignKey(appUserGroup=>appUserGroup.GroupId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(group => group.Feeds)
            .WithOne(appUserGroup => appUserGroup.Group)
            .HasForeignKey(appUserGroup => appUserGroup.GroupId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
