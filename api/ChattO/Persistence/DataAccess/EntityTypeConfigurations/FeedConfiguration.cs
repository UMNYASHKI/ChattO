using Domain.Models;
using Domain.Models.Files;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.DataAccess.EntityTypeConfigurations;

public class FeedConfiguration : IEntityTypeConfiguration<Feed>
{
    public void Configure(EntityTypeBuilder<Feed> builder)
    {
        builder.HasOne(feed => feed.FeedImage)
            .WithOne(img => img.Feed)
            .HasForeignKey<FeedImage>(img => img.FeedId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(feed=>feed.AppUserFeeds)
            .WithOne(appUserFeed=>appUserFeed.Feed)
            .HasForeignKey(appUserFeed=>appUserFeed.FeedId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
