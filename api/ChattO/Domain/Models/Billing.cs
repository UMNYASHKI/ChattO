namespace Domain.Models;

public class Billing
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public BillingStatus Status { get; set; } = BillingStatus.Pending;
    public Guid OrganizationId { get; set; }
    public virtual Organization Organization { get; set; }
    public Guid BillingInfoId { get; set; }
    public virtual BillingInfo BillingInfo { get; set; }
}
