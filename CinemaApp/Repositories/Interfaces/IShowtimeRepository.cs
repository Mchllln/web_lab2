using CinemaApp.Models;

namespace CinemaApp.Repositories.Interfaces;

/// <summary>Data-access contract for the auxiliary Showtime entity.</summary>
public interface IShowtimeRepository
{
    Task<IEnumerable<Showtime>> GetAllAsync();
    Task<Showtime?> GetByIdAsync(int id);
}
