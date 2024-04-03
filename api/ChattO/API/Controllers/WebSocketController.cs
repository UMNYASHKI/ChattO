using API.Helpers;
using Application.Helpers;
using Infrastructure.Services.WebSockets;
using Microsoft.AspNetCore.Mvc;
using System.Net.WebSockets;

namespace API.Controllers;

[Route("api/ws")]
[ApiController]
public class WebSocketController : Controller
{
    private readonly WebSocketHandler _webSocketHandler;

    private WebSocket _webSocket;

    public WebSocketController(WebSocketHandler webSocketHandler)
    {
        _webSocketHandler = webSocketHandler;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        if (!HttpContext.WebSockets.IsWebSocketRequest)
        {
            return BadRequest("Accepts only web socket requests");
        }

        _webSocket = await HttpContext.WebSockets.AcceptWebSocketAsync();
        var connectResult = await _webSocketHandler.OnConnected(_webSocket);

        var result = await ReceiveAsync(async (result, buffer) =>
        {
            if (result.MessageType == WebSocketMessageType.Close)
            {
                await _webSocketHandler.OnDisconnected(_webSocket);
                await _webSocket.CloseAsync(result.CloseStatus.Value, result.CloseStatusDescription, CancellationToken.None);
            }
            else if (result.MessageType == WebSocketMessageType.Text)
            {
                var handleMessageResult = await _webSocketHandler.HandleMessageText(result, buffer);
                //if (!handleMessageResult.IsSuccessful)
                //{
                //    return handleMessageResult;
                //}
                //return Result.Failure<bool>("");
            }
            else
            {
                //get feedId from query GUID
                // feedId through query string
                //binary
                //return Result.Failure<bool>("");
            }
        });

        if (!result.IsSuccessful)
        {
            return BadRequest(result.Message);
        }

        return NoContent();
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
            await _webSocket.CloseAsync(WebSocketCloseStatus.InternalServerError, ex.Message, CancellationToken.None);

            return Result.Failure<bool>(ex.Message);
        }
    }

    private async Task FailResonse(HttpContext context, string message)
    {
        context.Response.StatusCode = 400;
        await context.Response.WriteAsync(message);
    }
}
