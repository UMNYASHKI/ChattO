namespace Domain.Models;

public class AppUserFeed
{
    public Guid Id { get; set; }
    public Guid? AppUserId { get; set; }
    public virtual AppUser AppUser { get; set; }
    public Guid FeedId { get; set; }
    public virtual Feed Feed { get; set; }
    public bool IsCreator { get; set; }
    public virtual ICollection<Message> Messages { get; set; }
}
