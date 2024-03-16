using System.ComponentModel.DataAnnotations;

namespace API.DTOs.Requests.User;

public class CreateUsersRequest
{
    [Required]
    public List<CreateUserRequest> Users { get; set; }

    [Required]
    public Guid OrganizationId { get; set; }
}
