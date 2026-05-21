using CinemaApp.Data;
using CinemaApp.Models;
using CinemaApp.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CinemaApp.Repositories;

/// <summary>
/// EF Core implementation of ITicketRepository.
/// Performs all direct database operations for the Ticket entity.
/// </summary>
public class TicketRepository : ITicketRepository
{
    private readonly AppDbContext _db;

    public TicketRepository(AppDbContext db) => _db = db;

    public async Task<IEnumerable<Ticket>> GetAllAsync() =>
        await _db.Tickets
                 .Include(t => t.Showtime)   // load Showtime for partial display on list page
                 .OrderBy(t => t.Id)
                 .ToListAsync();

    public async Task<Ticket?> GetByIdAsync(int id) =>
        await _db.Tickets
                 .Include(t => t.Showtime)   // load Showtime for full display on form page
                 .FirstOrDefaultAsync(t => t.Id == id);

    public async Task AddAsync(Ticket ticket)
    {
        _db.Tickets.Add(ticket);
        await _db.SaveChangesAsync();
    }

    public async Task UpdateAsync(Ticket ticket)
    {
        _db.Tickets.Update(ticket);
        await _db.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var ticket = await _db.Tickets.FindAsync(id)
            ?? throw new KeyNotFoundException($"Ticket with id {id} not found.");
        _db.Tickets.Remove(ticket);
        await _db.SaveChangesAsync();
    }
}
