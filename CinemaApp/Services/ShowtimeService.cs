using CinemaApp.Models;
using CinemaApp.Repositories.Interfaces;
using CinemaApp.Services.Interfaces;

namespace CinemaApp.Services;

/// <summary>
/// Business Logic Layer for Showtime (read-only from the UI perspective).
/// </summary>
public class ShowtimeService : IShowtimeService
{
    private readonly IShowtimeRepository _showtimeRepository;

    public ShowtimeService(IShowtimeRepository showtimeRepository) =>
        _showtimeRepository = showtimeRepository;

    public Task<IEnumerable<Showtime>> GetAllShowtimesAsync() =>
        _showtimeRepository.GetAllAsync();

    public Task<Showtime?> GetShowtimeByIdAsync(int id) =>
        _showtimeRepository.GetByIdAsync(id);
}
