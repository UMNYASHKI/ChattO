using Application.Feeds.Queries;
using Application.Helpers;
using Infrastructure.Services.WebSockets;
using MediatR;

namespace Infrastructure.Services.Firebase;

public class FirebaseMessagingService 
{
    private readonly IMediator _mediator;
    private readonly ConnectionManager _connectionManager;

    public FirebaseMessagingService(IMediator mediator, ConnectionManager connectionManager)
    {
        _mediator = mediator;
        _connectionManager = connectionManager;
    }

    public async Task<Result<List<string>>> GetInActiveUsersDeviceTokens(Guid feedId)
    {
        var feedResult = await _mediator.Send(new GetDetailsFeed.Query { Id = feedId });
        if (!feedResult.IsSuccessful)
            return Result.Failure<List<string>>("Failed to get feed");

        var activeConnections = _connectionManager.GetActiveUserIds();
        var userFeeds = feedResult.Data.AppUserFeeds
            .Where(f => f.FeedId == feedId); //it's really needed??

        var inActiveUserIds = userFeeds
            .Select(x => x.AppUserId)
            .Except(activeConnections);

        var inActiveUserTokens = userFeeds
            .Where(f => inActiveUserIds.Contains(f.AppUserId))
            .Select(u => u.AppUser.DeviceToken)
            .ToList();
        
        return Result.Success(inActiveUserTokens);
    }
}
