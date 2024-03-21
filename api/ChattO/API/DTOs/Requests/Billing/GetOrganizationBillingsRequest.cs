using API.DTOs.Paging;
using API.DTOs.Sorting;

namespace API.DTOs.Requests.Billing;

public class GetOrganizationBillingsRequest : SortingParams
{
    public Guid OrganizationId { get; set; }
}
