namespace Domain.Models.Files;

public class ProfileImage : BaseFile
{
    public Guid AppUserId { get; set; }
    public virtual AppUser AppUser { get; set; }
}
