namespace Domain.Models;

public class Group
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Guid OrganizationId { get; set; }
    public virtual Organization Organization { get; set; }
    public virtual ICollection<AppUserGroup> AppUserGroups { get; set; }
    public virtual ICollection<Feed> Feeds { get; set; }
}
