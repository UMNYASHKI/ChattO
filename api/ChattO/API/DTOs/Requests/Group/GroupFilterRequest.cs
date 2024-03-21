using API.DTOs.Sorting;

namespace API.DTOs.Requests.Group;

public class GroupFilterRequest : SortingParams
{
    public string? Name { get; set; }
    public Guid? OrganizationId { get; set; }
}
