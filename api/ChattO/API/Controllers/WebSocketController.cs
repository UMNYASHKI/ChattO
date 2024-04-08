using API.Helpers;
using Application.Helpers;
using Infrastructure.Services.WebSockets;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.WebSockets;

namespace API.Controllers;

[Route("api/ws")]
[ApiController]
[Authorize]
public class WebSocketController : Controller
{
    private readonly WebSocketHandler _webSocketHandler;

    private WebSocket _webSocket;

    public WebSocketController(WebSocketHandler webSocketHandler)
    {
        _webSocketHandler = webSocketHandler;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
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
                if (!handleMessageResult.IsSuccessful)
                {
                    return handleMessageResult;
                }
            }
            else
            {
                //get feedId from query GUID
                // feedId through query string
                //binary
                //return Result.Failure<bool>("");
            }

            return Result.Success<bool>();
        });

        if (!result.IsSuccessful)
        {
            return BadRequest(result.Message);
        }

        return NoContent();
    }

    private async Task<Result<bool>> ReceiveAsync(Func<WebSocketReceiveResult, byte[], Task<Result<bool>>> handleMessage)
    {
        var buffer = new byte[WebSocketOptionsConstants.ReceiveBufferSize];

        try
        {
            while (_webSocket.State == WebSocketState.Open)
            {
                var receiveResult = await _webSocket.ReceiveAsync(buffer, CancellationToken.None);

                var handleResult = await handleMessage(receiveResult, buffer);
                if (!handleResult.IsSuccessful)
                {
                    return Result.Failure<bool>(handleResult.Message);
                }
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
}
