using CinemaApp.Data;
using CinemaApp.Repositories;
using CinemaApp.Repositories.Interfaces;
using CinemaApp.Services;
using CinemaApp.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// ── Services ─────────────────────────────────────────────────────────────────

builder.Services.AddControllersWithViews();

// Database (SQLite via EF Core)
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// Repository layer (DAO)
builder.Services.AddScoped<ITicketRepository, TicketRepository>();
builder.Services.AddScoped<IShowtimeRepository, ShowtimeRepository>();

// Service layer (Business Logic)
builder.Services.AddScoped<ITicketService, TicketService>();
builder.Services.AddScoped<IShowtimeService, ShowtimeService>();

// ── Pipeline ──────────────────────────────────────────────────────────────────

var app = builder.Build();

// Apply pending EF migrations and seed the DB automatically on startup
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
}

// Lab requirement: show a safe generic error page instead of a stack trace
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}
else
{
    // In development we still want a custom error page (not the developer exception page)
    // to demonstrate correct error handling behavior
    app.UseExceptionHandler("/Error");
}

app.UseStaticFiles();
app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Ticket}/{action=Index}/{id?}");

app.Run();
