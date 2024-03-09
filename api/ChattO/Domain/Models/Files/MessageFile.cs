namespace Domain.Models.Files;

public class MessageFile : BaseFile
{
    public Guid MessageId { get; set; }
    public virtual Message Message { get; set; }
}
