using Application.Abstractions;
using Domain.Models;
using MediatR;

namespace Infrastructure.Services.WebSockets;

public class WebSocketService
{
    private readonly IMediator _mediator;
    public WebSocketService(IMediator mediator)
    {
        _mediator = mediator;
    }
}
