using API.DTOs.Sorting;

namespace API.DTOs.Requests.Organization;

public class OrganizationFilteringRequest : SortingParams
{
    public string? Name { get; set; }
    public string? Domain { get; set; }
}
