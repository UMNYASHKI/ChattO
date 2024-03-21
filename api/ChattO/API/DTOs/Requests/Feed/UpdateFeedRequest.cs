namespace API.DTOs.Requests.Feed;

public class UpdateFeedRequest
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public IFormFile? FeedImage { get; set; }
}
