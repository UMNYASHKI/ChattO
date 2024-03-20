using Domain.Enums;

namespace API.DTOs.Requests.User;

public class CreateAccountsRequest
{
    public List<CreateAccountRequest> Requests { get; set; }

    public AppUserRole AppUserRole { get; set; }

    public Guid OrganizationId { get; set; }
}
