using API.DTOs.Paging;
using API.DTOs.Sorting;

namespace API.DTOs.Requests.BillingInfo;

public class GetBillingInfosRequest
{
    public SortingParams? SortingParams { get; set; }

    public PagingParams? PagingParams { get; set; }
}
