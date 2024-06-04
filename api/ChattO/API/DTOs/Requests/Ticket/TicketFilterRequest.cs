using API.DTOs.Paging;
using API.DTOs.Sorting;
using Domain.Enums;

namespace API.DTOs.Requests.Ticket;

public class TicketFilterRequest : SortingParams
{
    public string? Text { get; set; }

    public DateTime? SentAt { get; set; }

    public TicketTheme? Theme { get; set; }

    public Guid? AppUserId { get; set; }
}
