using System.ComponentModel.DataAnnotations;

namespace CinemaApp.Models;

/// <summary>
/// Auxiliary entity. Seeded on startup; cannot be created via the web UI.
/// One Showtime → many Tickets (1 : N).
/// </summary>
public class Showtime
{
    public int Id { get; set; }

    [Required, MaxLength(200)]
    public string MovieTitle { get; set; } = string.Empty;

    [Required]
    public DateTime StartTime { get; set; }

    [Required, MaxLength(100)]
    public string HallName { get; set; } = string.Empty;

    // Navigation property
    public ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();

    /// <summary>Full description used in dropdown lists.</summary>
    public string FullDescription =>
        $"{MovieTitle} | {StartTime:dd.MM.yyyy HH:mm} | {HallName}";
}
