using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CinemaApp.Models;

/// <summary>
/// Primary entity. Full CRUD operations are implemented for Ticket.
/// </summary>
public class Ticket
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Seat number is required.")]
    [Range(1, 500, ErrorMessage = "Seat number must be between 1 and 500.")]
    [Display(Name = "Seat Number")]
    public int SeatNumber { get; set; }

    [Required(ErrorMessage = "Price is required.")]
    [Range(0.01, 10_000, ErrorMessage = "Price must be greater than 0.")]
    [Column(TypeName = "decimal(10,2)")]
    public decimal Price { get; set; }

    [Required(ErrorMessage = "Customer name is required.")]
    [MaxLength(150, ErrorMessage = "Name must not exceed 150 characters.")]
    [Display(Name = "Customer Name")]
    public string CustomerName { get; set; } = string.Empty;

    // Foreign key
    [Required(ErrorMessage = "Please select a showtime.")]
    [Display(Name = "Showtime")]
    public int ShowtimeId { get; set; }

    // Navigation property
    public Showtime? Showtime { get; set; }
}
