using Domain.Models;

namespace Application.Abstractions
{
    public interface ITokenService
    {
        string GenerateToken(AppUser user);
    }
}
