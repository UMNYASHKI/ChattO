using Domain.Models;
using Domain.Models.Files;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Persistence.DataAccess.EntityTypeConfigurations;

namespace Persistence.DataAccess;

public class ChattoDbContext : IdentityDbContext<AppUser, IdentityRole<Guid>, Guid>
{
    public ChattoDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfiguration(new AppUserConfiguration());
        builder.ApplyConfiguration(new AppUserFeedConfiguration());
        builder.ApplyConfiguration(new FeedImageConfiguration());
        builder.ApplyConfiguration(new GroupConfiguration());
        builder.ApplyConfiguration(new FeedConfiguration());
        builder.ApplyConfiguration(new MessageFileConfiguration());
        builder.ApplyConfiguration(new OrganizationConfiguration());
        builder.ApplyConfiguration(new ProfileImageConfiguration());

        base.OnModelCreating(builder);
    }

    public DbSet<Organization> Organizations { get; set; }

    public DbSet<Ticket> Tickets { get; set; }

    public DbSet<AppUserFeed> AppUserFeeds { get; set; }

    public DbSet<AppUserGroup> AppUserGroups { get; set; }

    public DbSet<Feed> Feeds { get; set; }

    public DbSet<Group> Groups { get; set; }

    public DbSet<Message> Messages { get; set; }

    public DbSet<FeedImage> FeedImages { get; set; }

    public DbSet<ProfileImage> ProfileImages { get; set; }

    public DbSet<MessageFile> MessageFiles { get; set; }
}
