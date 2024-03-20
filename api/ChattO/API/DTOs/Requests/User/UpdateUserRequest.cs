using Application.Helpers.Mappings;
using Domain.Models;

namespace API.DTOs.Requests.User;

public class UpdateUserRequest :IMapWith<AppUser>
{
    public string? DisplayName { get; set; }

    public string? Email { get; set; }

    public string? UserName { get; set; }

    public Guid? ProfileImageId { get; set; }
}
