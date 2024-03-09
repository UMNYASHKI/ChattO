using Domain.Models.Files;

namespace Domain.Models;

public class Message
{
    public Guid Id { get; set; }
    public string Text { get; set; }
    public DateTime CreatedAt { get; set; }
    public bool IsRead { get; set; }
    public bool IsPinned { get; set; }
    public Guid AppUserFeedId { get; set; }
    public virtual AppUserFeed AppUserFeed { get; set; }
    public Guid MessageFileId { get; set; }
    public virtual MessageFile MessageFile { get; set; }
}
