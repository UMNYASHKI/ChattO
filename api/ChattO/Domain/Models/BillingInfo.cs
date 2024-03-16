using Domain.Enums;

namespace Domain.Models;

public class BillingInfo
{
    public Guid Id { get; set; }
    public BillingType Type { get; set; }
    public double Price { get; set; }
    public virtual ICollection<Billing> Billings { get; set; }
}
