using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.DataAccess.EntityTypeConfigurations;

public class AppUserFeedConfiguration : IEntityTypeConfiguration<AppUserFeed>
{
    public void Configure(EntityTypeBuilder<AppUserFeed> builder)
    {
        builder.HasMany(appUserFeed=>appUserFeed.Messages)
            .WithOne(message=>message.AppUserFeed)
            .HasForeignKey(message=>message.AppUserFeedId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
