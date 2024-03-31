using System.Collections.Concurrent;
using System.Net.WebSockets;

namespace Infrastructure.Services.WebSockets;

public class ConnectionManager
{
    private ConcurrentDictionary<Guid, WebSocket> _sockets = new();
    private ConcurrentDictionary<Guid, Guid> _users = new();

}
