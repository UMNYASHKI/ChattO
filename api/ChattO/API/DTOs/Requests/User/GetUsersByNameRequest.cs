using API.DTOs.Paging;
using API.DTOs.Sorting;

namespace API.DTOs.Requests.User;

public class GetUsersByNameRequest : SortingParams
{
    public string DisplayName { get; set; }
}
