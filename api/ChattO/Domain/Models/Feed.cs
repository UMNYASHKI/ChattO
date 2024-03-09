using Domain.Enums;
using Domain.Models.Files;

namespace Domain.Models;

public class Feed
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public FeedType Type { get; set; }
    public Guid? GroupId { get; set; }
    public virtual Group Group { get; set; }
    public Guid FeedImageId { get; set; }
    public virtual FeedImage FeedImage { get; set; }
    public virtual ICollection<AppUserFeed> AppUserFeeds { get; set; }
}
