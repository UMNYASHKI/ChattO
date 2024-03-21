namespace API.DTOs.Responses.Feed;

public class FeedAppUserResponse
{
    public Guid Id { get; set; }
    public string DisplayName { get; set; }
    public string Email { get; set; }
    public bool IsModerator { get; set; }
    public bool IsCreator { get; set; }
}
