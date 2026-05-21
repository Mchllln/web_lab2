using CinemaApp.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CinemaApp.ViewModels;

/// <summary>
/// Passed to the multipurpose Form view for Create, Edit, and Details modes.
/// Carries both the ticket being edited and the list of available showtimes
/// (displayed fully in the dropdown — lab requirement).
/// </summary>
public class TicketFormViewModel
{
    public Ticket Ticket { get; set; } = new();

    /// <summary>
    /// SelectList items show full Showtime detail: Movie | Date/Time | Hall.
    /// Lab requirement: auxiliary entity info shown completely on form page.
    /// </summary>
    public IEnumerable<SelectListItem> ShowtimeOptions { get; set; } =
        Enumerable.Empty<SelectListItem>();

    public bool IsEdit => Ticket.Id != 0;

    public string PageTitle => IsEdit ? $"Edit Ticket #{Ticket.Id}" : "New Ticket";
    public string SubmitLabel => IsEdit ? "Save Changes" : "Create Ticket";
}
