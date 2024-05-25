using Domain.Enums;

namespace API.Helpers;

public class RolesConstants
{
    public const string SystemAdmin = nameof(AppUserRole.SystemAdmin);

    public const string SuperAdmin = nameof(AppUserRole.SuperAdmin);

    public const string Admin = nameof(AppUserRole.Admin);

    public const string User = nameof(AppUserRole.User);
}
