using Application.Helpers.Mappings;

namespace API.DTOs.Requests.Billing;

public class CreateBillingRequest : IMapWith<Domain.Models.Billing>
{
    public Guid OrganizationId { get; set; }

    public Guid BillingInfoId { get; set; }
}
