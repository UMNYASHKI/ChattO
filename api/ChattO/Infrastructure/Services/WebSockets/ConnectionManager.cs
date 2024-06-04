using Application.Helpers;
using System.Collections.Concurrent;
using System.Net.WebSockets;

namespace Infrastructure.Services.WebSockets;

public class ConnectionManager
{
    private ConcurrentDictionary<WebSocket, Guid> _sockets = new();//socketId
    private ConcurrentDictionary<Guid, Guid> _users = new();//socketId, userId

    public Result<bool> AddConnection(WebSocket webSocket, Guid userId)
    {
        try
        {
            var connectionId = CreateConnectionId();
            var addSocketResult = _sockets.TryAdd(webSocket, connectionId);
            if (!addSocketResult)
                return Result.Failure<bool>("Failed to add connection");

            var addUserResult = _users.TryAdd(connectionId, userId);
            if (!addUserResult)
                return Result.Failure<bool>("Failed to add connection");

            return Result.Success<bool>();
        }
        catch
        {
            return Result.Failure<bool>("Failed to add connection");
        }
    }

    public Result<bool> RemoveConnection(WebSocket socket)
    {
        try
        {
            var removeSocketResult = _sockets.TryRemove(socket, out var socketId);
            if (!removeSocketResult)
                return Result.Failure<bool>("Failed to remove connection");

            var removeUserResult = _users.TryRemove(socketId, out var userId);
            if (!removeUserResult)
                return Result.Failure<bool>("Failed to remove connection");

            return Result.Success<bool>();
        }
        catch
        {
            return Result.Failure<bool>("Failed to remove connection");
        }
    }

    public List<Guid?> GetActiveUserIds()
    {
        return (List<Guid?>)[.. _users.Values];
    }

    public Result<List<WebSocket>> GetWebSockets(List<Guid> userIds)
    {
        try
        {
            var socketIds = _users.Where(x => userIds.Contains(x.Value))
                .Select(x => x.Key).ToList();
            var webSockets = _sockets.Where(x => socketIds.Contains(x.Value));

            return Result.Success(webSockets.Select(x => x.Key).ToList());
        }
        catch
        {
            return Result.Failure<List<WebSocket>>("Failed to get websockets");
        }
    }

    public Result<Guid> GetUserIdBySocket(WebSocket socket)
    {
        try
        {
            var webSocketId = _sockets.FirstOrDefault(x => x.Key.Equals(socket)).Value;
            var userId = _users.FirstOrDefault(x => x.Key == webSocketId).Value;
            if (webSocketId.Equals(Guid.Empty) || userId.Equals(Guid.Empty))
                return Result.Failure<Guid>("Failed to get userId by websocket");
            
            return Result.Success(userId);
        }
        catch
        {
            return Result.Failure<Guid>("Failed to get userId by websocket");
        }
    }

    private Guid CreateConnectionId()
    {
        return Guid.NewGuid();
    }
}
