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

    private Guid CreateConnectionId()
    {
        return Guid.NewGuid();
    }
}
