using System.ComponentModel.DataAnnotations;

namespace TicketApi.Models;

public enum TicketStatus
{
    Open,
    InProgress,
    Resolved
}

public class Ticket
{
    public int Id { get; set; }

    [Required]
    [StringLength(100, MinimumLength = 3)]
    public string Title { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    public string CustomerEmail { get; set; } = string.Empty;

    // 1 = most urgent, 5 = least urgent
    [Range(1, 5)]
    public int Priority { get; set; } = 3;

    public TicketStatus Status { get; set; } = TicketStatus.Open;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
