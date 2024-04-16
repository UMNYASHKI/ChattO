using Application.Abstractions;
using Application.Helpers;
using Infrastructure.DTOs.WebSockets;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json;

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

    public async Task<Result<bool>> HandleMessageText(WebSocketReceiveResult result, byte[] buffer)
    {
        var recivedDataEntity = JsonSerializer.Deserialize<ClientMessage>(ReceiveString(result, buffer));

        var activeConnectionsResult = await _webSocketService.GetActiveConnections(recivedDataEntity.FeedId);
        if (!activeConnectionsResult.IsSuccessful)
            return Result.Failure<bool>("Failed to get active connections");

        var saveMessageResult = await _webSocketService.SaveMessage(recivedDataEntity);
        if (!saveMessageResult.IsSuccessful)
            return Result.Failure<bool>("Failed to save message");

        var serverMessage = new ServerMessage()
        {
            SenderId = recivedDataEntity.SenderId,
            Content = recivedDataEntity.Content,
            FeedId = recivedDataEntity.FeedId
        };

        var broadcastResult = await BroadcastMessage(serverMessage, activeConnectionsResult.Data);
        if (!broadcastResult.IsSuccessful)
            return Result.Failure<bool>("Failed to broadcast message");

        return Result.Success<bool>();
    }

    public async Task<Result<bool>> HandleMessageBinary(byte[] buffer, WebSocket socket, Guid feedId)
    {
        // socket -> socketId, socketId -> userId, userId -> userEntity, userEntity -> domain
        var userResult = await _webSocketService.GetAppUserBySocket(socket);
        if (!userResult.IsSuccessful)
            return Result.Failure<bool>("Failed to get user by socket");

        var domain = userResult.Data.Organization.Domain;

        // CloudRepository.UploadFile(buffer, feedId, domain);

        var activeConnectionsResult = await _webSocketService.GetActiveConnections(feedId);
        if (!activeConnectionsResult.IsSuccessful)
            return Result.Failure<bool>("Failed to get active connections");

        //send file or serverBinaryMessage (two byte[] )?? to all active connections

        return Result.Success<bool>();
    }

    public async Task SendMessageAsync(WebSocket socket, ServerMessage serverMessage)
    {
        var serializedMessage = JsonSerializer.Serialize(serverMessage);
        var bytes = Encoding.UTF8.GetBytes(serializedMessage);
        await socket.SendAsync(new ArraySegment<byte>(bytes, 0, bytes.Length), WebSocketMessageType.Text, true, CancellationToken.None);
    }

    public async Task<Result<bool>> BroadcastMessage(ServerMessage serverMessage, List<WebSocket> sockets)
    {
        try
        {
            foreach (var socket in sockets)
            {
                await SendMessageAsync(socket, serverMessage);
            }

            return Result.Success<bool>();
        }
        catch
        {
            return Result.Failure<bool>("Failed to broadcast message");
        }
    }

    private string ReceiveString(WebSocketReceiveResult result, byte[] buffer)
    {
        return Encoding.UTF8.GetString(buffer, 0, result.Count);
    }
}
