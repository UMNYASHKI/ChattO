using Domain.Models;
using Domain.Models.Files;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.DataAccess.EntityTypeConfigurations;

public class FeedImageConfiguration : IEntityTypeConfiguration<FeedImage>
{
    public void Configure(EntityTypeBuilder<FeedImage> builder)
    {
        builder.HasOne(feedImage => feedImage.Feed)
            .WithOne(feed => feed.FeedImage)
            .HasForeignKey<Feed>(feed => feed.FeedImageId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
