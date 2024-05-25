namespace Infrastructure.DTOs.WebSockets;

public class ClientMessage
{
    public Guid SenderId { get; set; }
    public Guid FeedId { get; set; }
    public string Content { get; set; }
}
