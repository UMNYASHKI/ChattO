using Application.Abstractions;
using Application.AppUsers.Queries;
using Application.Feeds.Queries;
using Application.Helpers;
using Application.Messages.Commands;
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
        var feedAppUersResult = await _mediator.Send(new GetFeedAppUsers.Query { FeedId = feedId });
        if (!feedAppUersResult.IsSuccessful)
            return Result.Failure<List<WebSocket>>(feedAppUersResult.Message);

        var userIds = feedAppUersResult.Data.Select(x => (Guid)x.AppUserId).ToList();
        var webSocketsResult = _connectionManager.GetWebSockets(userIds);
        if (!webSocketsResult.IsSuccessful)
            return Result.Failure<List<WebSocket>>(webSocketsResult.Message);
        
        return Result.Success(webSocketsResult.Data);
    } 

    public async Task<Result<bool>> SaveMessage(ClientMessage message)
    {
        var saveMessageResult = await _mediator.Send(new CreateMessage.Command { Content = message.Content, 
                FeedId = message.FeedId, 
                SenderId = message.SenderId});
        if (!saveMessageResult.IsSuccessful)
            return Result.Failure<bool>(saveMessageResult.Message);

        return Result.Success(true);
    }

    public async Task<Result<AppUser>> GetAppUserBySocket(WebSocket socket)
    {
        var userIdResult = _connectionManager.GetUserIdBySocket(socket);
        if (!userIdResult.IsSuccessful)
            return Result.Failure<AppUser>("UserId not found");

        var appUserResult = await _mediator.Send(new GetDetailsAppUser.Query { Id = userIdResult.Data });
        if (!appUserResult.IsSuccessful)
            return Result.Failure<AppUser>(appUserResult.Message);

        return Result.Success(appUserResult.Data);
    }
}
