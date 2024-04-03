﻿using Application.Abstractions;
using Application.Helpers;
using Domain.Models;
using Infrastructure.DTOs.WebSockets;
using MediatR;
using System.Net.WebSockets;

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

    public async Task<Result<List<WebSocket>>> GetActiveConnections(Guid feedId) 
    {
        throw new NotImplementedException();
    } 

    public async Task<Result<bool>> SaveMessage(ClientMessage message)
    {
        throw new NotImplementedException();
    }

    public async Task<Result<AppUser>> GetAppUserBySocket(WebSocket socket)
    {
        throw new NotImplementedException();
    }
}