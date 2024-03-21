namespace API.DTOs.Responses.Group;

public class GroupResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public List<AppUserGroupResponse> Users { get; set; }
}
