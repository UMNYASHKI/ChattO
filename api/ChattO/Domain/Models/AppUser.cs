using Domain.Enums;
using Domain.Models.Files;
using Microsoft.AspNetCore.Identity;

namespace Domain.Models;

public class AppUser : IdentityUser<Guid>
{
    public string DisplayName { get; set; }
    public AppUserRole Role { get; set; }
    public bool IsEmailSent { get; set; }
    public Guid OrganizationId { get; set; }
    public virtual Organization Organization { get; set; }
    public Guid ProfileImageId { get; set; }
    public virtual ProfileImage ProfileImage { get; set; }
    public virtual ICollection<AppUserGroup> AppUserGroups { get; set; }
    public virtual ICollection<AppUserFeed> AppUserFeeds { get; set; }
    public virtual ICollection<Ticket> Tickets { get; set; }
}
