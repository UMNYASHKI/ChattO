using API.Helpers;

namespace API.Extensions;

public static class WebSocketExtension
{
    public static WebApplication AddWebSocket(this WebApplication app)
    {
        var webSocketOptions = new WebSocketOptions()
        {
            KeepAliveInterval = TimeSpan.FromSeconds(WebSocketOptionsConstants.KeepAliveInterval)
        };

        app.UseWebSockets(webSocketOptions);

        return app;
    }
}
