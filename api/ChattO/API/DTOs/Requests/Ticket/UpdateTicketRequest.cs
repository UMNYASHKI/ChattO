using Application.Helpers.Mappings;
using Domain.Enums;

namespace API.DTOs.Requests.Ticket;

public class UpdateTicketRequest : IMapWith<Domain.Models.Ticket>
{
    public string? Text { get; set; }

    public TicketTheme? Theme { get; set; }

    public TicketStatus? Status { get; set; }
}
