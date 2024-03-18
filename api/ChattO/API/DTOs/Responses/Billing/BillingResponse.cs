using Application.Helpers.Mappings;

namespace API.DTOs.Responses.Billing;

public class BillingResponse : IMapWith<Domain.Models.Billing>
{
    public Guid Id { get; set; }

    public DateTime CreatedAt { get; set; }

    public Guid OrganizationId { get; set; }

    public BillingInfoResponse BillingInfo { get; set; }
}
