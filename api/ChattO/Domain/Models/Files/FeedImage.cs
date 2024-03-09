namespace Domain.Models.Files;

public class FeedImage : BaseFile
{
    public Guid FeedId { get; set; }
    public virtual Feed Feed { get; set; }
}
