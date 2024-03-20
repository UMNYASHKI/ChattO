using API.DTOs.Responses.File;
using Application.Helpers.Mappings;
using Domain.Enums;
using Domain.Models;

namespace API.DTOs.Responses.User;

public class UserResponse : IMapWith<AppUser>
{
    public Guid Id { get; set; }

    public string UserName { get; set; }

    public string Email { get; set; }

    public string DisplayName { get; set; }

    public AppUserRole Role { get; set; }

    public bool IsEmailSent { get; set; }

    public FileResponse ProfileImage { get; set; }
}
