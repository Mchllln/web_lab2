using CinemaApp.Models;
using CinemaApp.Services.Interfaces;
using CinemaApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CinemaApp.Controllers;

/// <summary>
/// Routing/API layer — handles all HTTP requests for the Ticket entity.
/// Delegates business logic to ITicketService and IShowtimeService.
/// </summary>
public class TicketController : Controller
{
    private readonly ITicketService   _ticketService;
    private readonly IShowtimeService _showtimeService;

    public TicketController(ITicketService ticketService, IShowtimeService showtimeService)
    {
        _ticketService   = ticketService;
        _showtimeService = showtimeService;
    }

    // ── GET /Ticket ──────────────────────────────────────────────────────────

    /// <summary>
    /// List page: shows all tickets.
    /// Showtime info is shown partially (Movie Title only) — lab requirement.
    /// </summary>
    public async Task<IActionResult> Index()
    {
        try
        {
            var tickets = await _ticketService.GetAllTicketsAsync();
            return View(tickets);
        }
        catch (Exception ex)
        {
            return HandleError(ex);
        }
    }

    // ── GET /Ticket/Create ────────────────────────────────────────────────────

    public async Task<IActionResult> Create()
    {
        try
        {
            return View("Form", await BuildFormViewModelAsync(new Ticket()));
        }
        catch (Exception ex)
        {
            return HandleError(ex);
        }
    }

    // ── POST /Ticket/Create ───────────────────────────────────────────────────

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Ticket ticket)
    {
        if (!ModelState.IsValid)
            return View("Form", await BuildFormViewModelAsync(ticket));

        try
        {
            await _ticketService.CreateTicketAsync(ticket);
            TempData["Success"] = "Ticket created successfully.";
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            return HandleError(ex);
        }
    }

    // ── GET /Ticket/Edit/{id} ─────────────────────────────────────────────────

    public async Task<IActionResult> Edit(int id)
    {
        try
        {
            var ticket = await _ticketService.GetTicketByIdAsync(id);
            if (ticket is null) return NotFound();

            return View("Form", await BuildFormViewModelAsync(ticket));
        }
        catch (Exception ex)
        {
            return HandleError(ex);
        }
    }

    // ── POST /Ticket/Edit/{id} ────────────────────────────────────────────────

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Ticket ticket)
    {
        if (id != ticket.Id) return BadRequest();

        if (!ModelState.IsValid)
            return View("Form", await BuildFormViewModelAsync(ticket));

        try
        {
            await _ticketService.UpdateTicketAsync(ticket);
            TempData["Success"] = "Ticket updated successfully.";
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            return HandleError(ex);
        }
    }

    // ── GET /Ticket/Details/{id} ──────────────────────────────────────────────

    /// <summary>
    /// Details (view-only) re-uses the same Form view in read-only mode.
    /// Lab requirement: single page for view, edit, and create.
    /// </summary>
    public async Task<IActionResult> Details(int id)
    {
        try
        {
            var ticket = await _ticketService.GetTicketByIdAsync(id);
            if (ticket is null) return NotFound();

            var vm = await BuildFormViewModelAsync(ticket);
            ViewBag.ReadOnly = true;
            return View("Form", vm);
        }
        catch (Exception ex)
        {
            return HandleError(ex);
        }
    }

    // ── POST /Ticket/Delete/{id} ──────────────────────────────────────────────

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await _ticketService.DeleteTicketAsync(id);
            TempData["Success"] = "Ticket deleted successfully.";
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            return HandleError(ex);
        }
    }

    // ── Helpers ───────────────────────────────────────────────────────────────

    private async Task<TicketFormViewModel> BuildFormViewModelAsync(Ticket ticket)
    {
        var showtimes = await _showtimeService.GetAllShowtimesAsync();

        // Lab requirement: dropdown shows full Showtime info (Movie | Date/Time | Hall)
        var options = showtimes.Select(s => new SelectListItem
        {
            Value    = s.Id.ToString(),
            Text     = s.FullDescription,
            Selected = s.Id == ticket.ShowtimeId
        });

        return new TicketFormViewModel { Ticket = ticket, ShowtimeOptions = options };
    }

    /// <summary>
    /// Converts unhandled exceptions into the generic error view.
    /// Lab requirement: users see "Error processing the request", not a stack trace.
    /// </summary>
    private IActionResult HandleError(Exception ex)
    {
        // Log in a real app; for the lab we keep it simple
        TempData["ErrorDetail"] = ex.Message;
        return RedirectToAction("Index", "Error");
    }
}
