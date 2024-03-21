using System.ComponentModel.DataAnnotations;

namespace API.DTOs.Requests.Group;

public class CreateGroupRequest
{
    [Required]
    public string Name { get; set; }
    [Required]
    public Guid OrganizationId { get; set; }
    public List<Guid> UsersId { get; set; }
}
