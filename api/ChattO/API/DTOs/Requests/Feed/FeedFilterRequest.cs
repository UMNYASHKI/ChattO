using API.DTOs.Sorting;
using Domain.Enums;

namespace API.DTOs.Requests.Feed;

public class FeedFilterRequest : SortingParams
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public FeedType? Type { get; set; }
    public Guid? GroupId { get; set; }
}
