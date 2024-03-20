using API.DTOs.Paging;
using API.DTOs.Sorting;
using Domain.Enums;

namespace API.DTOs.Requests.Ticket;

public class TicketFilterRequest
{
    public string? Text { get; set; }

    public DateTime? SentAt { get; set; }

    public TicketTheme? Theme { get; set; }

    public Guid? AppUserId { get; set; }

    public PagingParams? PagingParams { get; set; }

    public SortingParams? SortingParams { get; set; }
}
