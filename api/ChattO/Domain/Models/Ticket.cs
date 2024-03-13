using Domain.Enums;

namespace Domain.Models;

public class Ticket
{
    public Guid Id { get; set; }
    public string Text { get; set; }
    public DateTime SentAt { get; set; }
    public TicketTheme Theme { get; set; }
    public TicketStatus Status { get; set; }
    public Guid AppUserId { get; set; }
    public virtual AppUser AppUser { get; set; }
    public Guid FeedId { get; set; }
    public virtual Feed Feed { get; set; }
}
