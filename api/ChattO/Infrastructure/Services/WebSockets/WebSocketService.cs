using Application.Abstractions;
using Domain.Models;
using MediatR;

namespace Infrastructure.Services.WebSockets;

public class WebSocketService
{
    private readonly IMediator _mediator;
    private readonly ConnectionManager _connectionManager;
    public WebSocketService(IMediator mediator, ConnectionManager connectionManager)
    {
        _mediator = mediator;
        _connectionManager = connectionManager;
    }
}
