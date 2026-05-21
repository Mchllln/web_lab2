using CinemaApp.Models;

namespace CinemaApp.Services.Interfaces;

/// <summary>Business-logic contract for the primary Ticket entity.</summary>
public interface ITicketService
{
    Task<IEnumerable<Ticket>> GetAllTicketsAsync();
    Task<Ticket?> GetTicketByIdAsync(int id);
    Task CreateTicketAsync(Ticket ticket);
    Task UpdateTicketAsync(Ticket ticket);
    Task DeleteTicketAsync(int id);
}
