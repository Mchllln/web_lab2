using CinemaApp.Models;

namespace CinemaApp.Services.Interfaces;

/// <summary>Business-logic contract for the auxiliary Showtime entity.</summary>
public interface IShowtimeService
{
    Task<IEnumerable<Showtime>> GetAllShowtimesAsync();
    Task<Showtime?> GetShowtimeByIdAsync(int id);
}
