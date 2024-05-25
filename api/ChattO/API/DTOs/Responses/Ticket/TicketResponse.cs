using Application.Helpers.Mappings;
using Domain.Enums;

namespace API.DTOs.Responses.Ticket;

public class TicketResponse : IMapWith<Domain.Models.Ticket>
{
    public Guid Id { get; set; }

    public string Text { get; set; }

    public DateTime SentAt { get; set; }

    public TicketTheme Theme { get; set; }

    public TicketStatus Status { get; set; }
}
