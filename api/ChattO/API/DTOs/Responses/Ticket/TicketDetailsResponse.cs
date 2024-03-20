using API.DTOs.Responses.User;
using Application.Helpers.Mappings;
using Domain.Enums;
using Domain.Models;

namespace API.DTOs.Responses.Ticket;

public class TicketDetailsResponse: IMapWith<Domain.Models.Ticket>
{
    public Guid Id { get; set; }

    public string Text { get; set; }

    public DateTime SentAt { get; set; }

    public TicketTheme Theme { get; set; }

    public TicketStatus Status { get; set; }

    public UserResponse AppUser { get; set; }

    //public FeedResponse Feed { get; set; }
}
