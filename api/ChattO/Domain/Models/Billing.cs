namespace Domain.Models;

public class Billing
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public Guid OrganizationId { get; set; }
    public virtual Organization Organization { get; set; }
    public Guid BillingInfoId { get; set; }
    public virtual BillingInfo BillingInfo { get; set; }
}
