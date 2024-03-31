namespace Infrastructure.Services.WebSockets;

public class WebSocketHandler
{
    private readonly WebSocketService _webSocketService;
    public WebSocketHandler(WebSocketService webSocketService)
    {
        _webSocketService = webSocketService;
    }
}
