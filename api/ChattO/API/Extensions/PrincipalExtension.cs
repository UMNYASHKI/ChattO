using System.Security.Claims;

namespace API.Extensions;

public static class PrincipalExtension
{
    public static Guid GetIdFromPrincipal(this ClaimsPrincipal principal) 
    {
        var userId = principal.FindFirstValue(ClaimTypes.NameIdentifier);
        ArgumentNullException.ThrowIfNull(userId);

        return Guid.Parse(userId);
    }
 }
