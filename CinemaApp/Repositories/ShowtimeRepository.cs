using CinemaApp.Data;
using CinemaApp.Models;
using CinemaApp.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CinemaApp.Repositories;

/// <summary>
/// EF Core implementation of IShowtimeRepository.
/// Read-only — showtimes are seeded and cannot be created via the web UI.
/// </summary>
public class ShowtimeRepository : IShowtimeRepository
{
    private readonly AppDbContext _db;

    public ShowtimeRepository(AppDbContext db) => _db = db;

    public async Task<IEnumerable<Showtime>> GetAllAsync() =>
        await _db.Showtimes.OrderBy(s => s.StartTime).ToListAsync();

    public async Task<Showtime?> GetByIdAsync(int id) =>
        await _db.Showtimes.FindAsync(id);
}
