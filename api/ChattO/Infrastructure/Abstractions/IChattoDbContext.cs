using Domain.Models;
using Domain.Models.Files;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Infrastructure.Abstractions;

public interface IChattoDbContext
{
    public DatabaseFacade Database { get; }
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
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    DbSet<TEntity> Set<TEntity>()
        where TEntity : class;
    EntityEntry<TEntity> Entry<TEntity>(TEntity entity)
        where TEntity : class;
}
