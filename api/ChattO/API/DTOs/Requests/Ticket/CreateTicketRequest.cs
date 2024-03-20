using Application.Helpers.Mappings;
using Domain.Enums;
using Domain.Models;

namespace API.DTOs.Requests.Ticket;

public class CreateTicketRequest : IMapWith<Domain.Models.Ticket>
{
    public string Text { get; set; }

    public TicketTheme? Theme { get; set; }
}
