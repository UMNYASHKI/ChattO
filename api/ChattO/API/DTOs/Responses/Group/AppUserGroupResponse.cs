namespace API.DTOs.Responses.Group;

public class AppUserGroupResponse
{
    public Guid Id { get; set; }
    public string DisplayName { get; set; }
    public string Email { get; set; }
    public bool IsModerator { get; set; }
    public Guid GroupId { get; set; }
}
