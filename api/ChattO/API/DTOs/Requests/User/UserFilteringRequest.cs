using Domain.Enums;

namespace API.DTOs.Requests.User;

public class UserFilteringRequest
{
    public Guid? GroupId { get; set; }

    public string? UserName { get; set; }

    public AppUserRole? AppUserRole { get; set; }

    public string? Email { get; set; }

    public Guid? OrganizationId { get; set; }

    public bool? IsEmailSent { get; set; }

    public string? DisplayName { get; set; }
}
