namespace Domain.Models;

public class AppUserGroup
{
    public Guid Id { get; set; }
    public bool IsModerator { get; set; }
    public Guid AppUserId { get; set; }
    public virtual AppUser AppUser { get; set; }
    public Guid GroupId { get; set; }
    public virtual Group Group { get; set; }
}
