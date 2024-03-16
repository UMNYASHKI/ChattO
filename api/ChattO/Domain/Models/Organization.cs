namespace Domain.Models;

public class Organization
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Domain { get; set; }
    public virtual ICollection<AppUser> AppUsers { get; set; }
    public virtual ICollection<Billing> Billings { get; set; }
}
