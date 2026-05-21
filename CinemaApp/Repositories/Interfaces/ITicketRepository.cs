using CinemaApp.Models;

namespace CinemaApp.Repositories.Interfaces;

/// <summary>Data-access contract for the primary Ticket entity.</summary>
public interface ITicketRepository
{
    Task<IEnumerable<Ticket>> GetAllAsync();
    Task<Ticket?> GetByIdAsync(int id);
    Task AddAsync(Ticket ticket);
    Task UpdateAsync(Ticket ticket);
    Task DeleteAsync(int id);
}
