namespace API.DTOs.Requests.Group;

public class UpdateGroupRequest
{
    public string? Name { get; set; }
    public List<Guid>? AddedUsersId { get; set; }
    public List<Guid>? RemovedUsersId { get; set; }
}
