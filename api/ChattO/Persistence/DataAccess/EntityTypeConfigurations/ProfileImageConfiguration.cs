using Domain.Models;
using Domain.Models.Files;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.DataAccess.EntityTypeConfigurations;

public class ProfileImageConfiguration : IEntityTypeConfiguration<ProfileImage>
{
    public void Configure(EntityTypeBuilder<ProfileImage> builder)
    {
        builder.HasOne(profileImage => profileImage.AppUser)
            .WithOne(profile => profile.ProfileImage)
            .HasForeignKey<AppUser>(profile => profile.ProfileImageId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
