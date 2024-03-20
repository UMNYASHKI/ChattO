using Application.Helpers.Mappings;
using Domain.Enums;
using Domain.Models;

namespace API.DTOs.Requests.User;

public class UserInOrganizationFilteringRequest : IMapWith<AppUser>
{
    public Guid? GroupId { get; set; }

    public string? UserName { get; set; }

    public string? Email { get; set; }

    public bool? IsEmailSent { get; set; }

    public string? DisplayName { get; set; }
}
