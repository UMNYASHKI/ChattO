using Application.Abstractions;
using Application.Helpers;
using System.Net.WebSockets;

namespace Infrastructure.Services.WebSockets;

public class WebSocketHandler
{
    private readonly WebSocketService _webSocketService;
    private readonly IUserService _userService;
    private readonly ConnectionManager _connectionManager;
    public WebSocketHandler(WebSocketService webSocketService, IUserService userService, ConnectionManager connectionManager)
    {
        _webSocketService = webSocketService;
        _userService = userService;
        _connectionManager = connectionManager;
    }

    public async Task<Result<bool>> OnConnected(WebSocket webSocket)
    {
        var userResult = await _userService.GetCurrentUser();
        if (!userResult.IsSuccessful)
            return Result.Failure<bool>("Failed to create connection");

        var createConnectionResult = _connectionManager.AddConnection(webSocket, userResult.Data.Id);
        if (!createConnectionResult.IsSuccessful)
            return Result.Failure<bool>("Failed to create connection");

        return Result.Success<bool>();
    }

    public async Task<Result<bool>> OnDisconnected(WebSocket webSocket)
    {
        var removeConnectionResult = _connectionManager.RemoveConnection(webSocket);
        if (!removeConnectionResult.IsSuccessful)
            return Result.Failure<bool>("Failed to remove connection");

        return Result.Success<bool>();
    }
}
