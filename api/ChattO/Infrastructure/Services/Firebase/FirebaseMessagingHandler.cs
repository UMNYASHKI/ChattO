using Application.Helpers;
using FirebaseAdmin.Messaging;
using Infrastructure.DTOs.WebSockets;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Services.Firebase;

public class FirebaseMessagingHandler
{
    private readonly FirebaseMessaging _messagingService = FirebaseMessaging.DefaultInstance;
    private readonly FirebaseMessagingService _firebaseMessagingService;

    public FirebaseMessagingHandler(FirebaseMessagingService firebaseMessagingService)
    {
        _firebaseMessagingService = firebaseMessagingService;
    }

    public async Task<Result<bool>> SendPushMessageAsync(ServerMessage message)
    {
        var deviceTokensResult = await _firebaseMessagingService.GetInActiveUsersDeviceTokens(message.FeedId);
        if (!deviceTokensResult.IsSuccessful)
            return Result.Failure<bool>("Failed to get device tokens");

        if (deviceTokensResult.Data.IsNullOrEmpty())
            return Result.Success(true);

        if (deviceTokensResult.Data.Count == 1)
        {
            var singleSentResult = await SendMessageToSingleDevice(message, deviceTokensResult.Data[0]);
            return singleSentResult;
        }
        else
        {
            var multipleSentResult = await SendMessageToMultipleDevices(message, deviceTokensResult.Data);
            return multipleSentResult;
        }      
    }

    private async Task<Result<bool>> SendMessageToSingleDevice(ServerMessage serverMessage, string deviceToken)
    {
        var message = new Message()
        {
            Data = GetNotificationPayload(serverMessage),
            Token = deviceToken,
        };

        var result = await _messagingService.SendAsync(message);

        return !result.IsNullOrEmpty() ? Result.Success(true) : Result.Failure<bool>("Failed to send message");
    }

    private async Task<Result<bool>> SendMessageToMultipleDevices(ServerMessage serverMessage, List<string> deviceTokens)
    {
        var message = new MulticastMessage()
        {
            Data = GetNotificationPayload(serverMessage),
            Tokens = deviceTokens,
        };

        var batchResult = await _messagingService.SendEachForMulticastAsync(message); 

        return batchResult.FailureCount < deviceTokens.Count ? Result.Success(true) : Result.Failure<bool>("Failed to send message");
    }

    private Dictionary<string, string> GetNotificationPayload(ServerMessage serverMessage)
    {
        return new Dictionary<string, string>()
        {
            [nameof(serverMessage.Content)] = serverMessage.Content,
            [nameof(serverMessage.SenderId)] = serverMessage.SenderId.ToString(),
            [nameof(serverMessage.FeedId)] = serverMessage.FeedId.ToString(),
        };
    }
}
