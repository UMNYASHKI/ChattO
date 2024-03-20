using API.DTOs.Paging;
using API.DTOs.Sorting;

namespace API.DTOs.Requests.User;

public class GetUsersByNameRequest
{
    public string DisplayName { get; set; }

    public SortingParams? SortingParams { get; set; }

    public PagingParams? PagingParams { get; set; }
}
