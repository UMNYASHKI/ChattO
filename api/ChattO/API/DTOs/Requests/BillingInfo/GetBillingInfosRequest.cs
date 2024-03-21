using API.DTOs.Paging;
using API.DTOs.Sorting;
using Domain.Enums;

namespace API.DTOs.Requests.BillingInfo;

public class GetBillingInfosRequest : SortingParams
{
    public BillingType? Type { get; set; }

    public double? MinPrice { get; set; }

    public double? MaxPrice { get; set; }
}
