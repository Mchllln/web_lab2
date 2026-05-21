using CinemaApp.Models;
using Microsoft.EntityFrameworkCore;

namespace CinemaApp.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Showtime> Showtimes => Set<Showtime>();
    public DbSet<Ticket>   Tickets   => Set<Ticket>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // ── Relationships ─────────────────────────────────────────────────────
        modelBuilder.Entity<Ticket>()
            .HasOne(t => t.Showtime)
            .WithMany(s => s.Tickets)
            .HasForeignKey(t => t.ShowtimeId)
            .OnDelete(DeleteBehavior.Restrict); // prevent cascade-deleting showtimes

        // ── Seed auxiliary entity (Showtime) ──────────────────────────────────
        // Lab requirement: auxiliary records must be pre-seeded;
        // users cannot create them via the web interface.
        modelBuilder.Entity<Showtime>().HasData(
            new Showtime
            {
                Id = 1,
                MovieTitle = "Inception",
                StartTime  = new DateTime(2026, 6, 1, 18, 30, 0),
                HallName   = "Hall A"
            },
            new Showtime
            {
                Id = 2,
                MovieTitle = "The Dark Knight",
                StartTime  = new DateTime(2026, 6, 1, 21, 0, 0),
                HallName   = "Hall B"
            },
            new Showtime
            {
                Id = 3,
                MovieTitle = "Interstellar",
                StartTime  = new DateTime(2026, 6, 2, 17, 0, 0),
                HallName   = "Hall A"
            },
            new Showtime
            {
                Id = 4,
                MovieTitle = "Oppenheimer",
                StartTime  = new DateTime(2026, 6, 2, 20, 0, 0),
                HallName   = "Hall C"
            },
            new Showtime
            {
                Id = 5,
                MovieTitle = "Dune: Part Two",
                StartTime  = new DateTime(2026, 6, 3, 19, 30, 0),
                HallName   = "Hall B"
            }
        );

        // Seed a few initial Ticket records for demonstration
        modelBuilder.Entity<Ticket>().HasData(
            new Ticket { Id = 1, SeatNumber = 12, Price = 8.50m,  CustomerName = "Alice Johnson", ShowtimeId = 1 },
            new Ticket { Id = 2, SeatNumber = 7,  Price = 10.00m, CustomerName = "Bob Smith",     ShowtimeId = 2 },
            new Ticket { Id = 3, SeatNumber = 24, Price = 8.50m,  CustomerName = "Carol White",   ShowtimeId = 1 }
        );
    }
}
