using CinemaApp.Models;
using CinemaApp.Repositories.Interfaces;
using CinemaApp.Services.Interfaces;

namespace CinemaApp.Services;

/// <summary>
/// Business Logic Layer for Ticket.
/// Delegates persistence to ITicketRepository and enforces domain rules.
/// </summary>
public class TicketService : ITicketService
{
    private readonly ITicketRepository _ticketRepository;

    public TicketService(ITicketRepository ticketRepository) =>
        _ticketRepository = ticketRepository;

    public Task<IEnumerable<Ticket>> GetAllTicketsAsync() =>
        _ticketRepository.GetAllAsync();

    public Task<Ticket?> GetTicketByIdAsync(int id) =>
        _ticketRepository.GetByIdAsync(id);

    public async Task CreateTicketAsync(Ticket ticket)
    {
        ArgumentNullException.ThrowIfNull(ticket);
        await _ticketRepository.AddAsync(ticket);
    }

    public async Task UpdateTicketAsync(Ticket ticket)
    {
        ArgumentNullException.ThrowIfNull(ticket);
        await _ticketRepository.UpdateAsync(ticket);
    }

    public Task DeleteTicketAsync(int id) =>
        _ticketRepository.DeleteAsync(id);
}
