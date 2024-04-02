namespace Infrastructure.DTOs.WebSockets;

public class ServerTextMessage
{
    public Guid SenderId { get; set; }
    public Guid FeedId { get; set; }
    public string Content { get; set; }
}
