using API.DTOs.Paging;
using API.DTOs.Sorting;

namespace API.DTOs.Requests.Billing;

public class GetOrganizationBillingsRequest
{
    public Guid OrganizationId { get; set; }

    public SortingParams SortingParams { get; set; }    

    public PagingParams PagingParams { get; set; }
}
