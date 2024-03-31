using Application.Helpers;
using Infrastructure.Services.WebSockets;
using System.Net.WebSockets;

namespace API.Helpers;

public class WebSocketMiddleware
{
    private readonly RequestDelegate _next;

    private readonly WebSocketHandler _webSocketHandler;

    private WebSocket _webSocket;

    public WebSocketMiddleware(RequestDelegate request, WebSocketHandler webSocketHandler)
    {
        _next = request;
        _webSocketHandler = webSocketHandler;
    }

    public async Task Invoke(HttpContext context)
    {
        if (!context.WebSockets.IsWebSocketRequest)
        {
            await _next(context);
            return;
        }

        _webSocket = await context.WebSockets.AcceptWebSocketAsync();
        var connectResult = await _webSocketHandler.OnConnected(_webSocket);

        var result = await ReceiveAsync(async (result, buffer) =>
        {
            if (result.MessageType == WebSocketMessageType.Close)
            {
                await _webSocketHandler.OnDisconnected(_webSocket);
            }
            else if (result.MessageType == WebSocketMessageType.Text)
            {

            }
            else
            {
                //binary
            }
        });

        if (!result.IsSuccessful)
        {
            context.Response.StatusCode = 400;
            await context.Response.WriteAsync(result.Message);
        }
    }

    private async Task<Result<bool>> ReceiveAsync(Action<WebSocketReceiveResult, byte[]> handleMessage)
    {
        var buffer = new byte[WebSocketOptionsConstants.ReceiveBufferSize];

        try
        {
            while (_webSocket.State == WebSocketState.Open)
            {
                var receiveResult = await _webSocket.ReceiveAsync(buffer, CancellationToken.None);

                handleMessage(receiveResult, buffer);
            }

            return Result.Success<bool>();
        }
        catch (Exception ex)
        {
            await _webSocketHandler.OnDisconnected(_webSocket);
            return Result.Failure<bool>(ex.Message);
        }
    }
}
