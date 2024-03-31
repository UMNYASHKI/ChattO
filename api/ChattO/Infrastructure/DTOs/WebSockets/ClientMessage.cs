namespace Infrastructure.DTOs.WebSockets;

public class ClientMessage
{
    public string Type { get; set; }

    public string Sender { get; set; }

    public string Receiver { get; set; }

    public string Content { get; set; }
}
