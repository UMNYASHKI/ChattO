using Domain.Models;

namespace API.Services.Abstractions
{
    public interface IJwtService
    {
        string GenerateJwtToken(AppUser user);
    }
}
