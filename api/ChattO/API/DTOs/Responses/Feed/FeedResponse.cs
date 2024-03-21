using Domain.Enums;

namespace API.DTOs.Responses.Feed;

public class FeedResponse
{
    public string Name { get; set; }
    public string Description { get; set; }
    public FeedType Type { get; set; }
    public Guid? GroupId { get; set; }
    public string? FeedImageUrl { get; set; } 
    public List<FeedAppUserResponse> AppUsers { get; set; }
}
