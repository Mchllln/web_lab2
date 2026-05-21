using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace CinemaApp.Controllers;

/// <summary>
/// Handles all unhandled exceptions and shows a safe, generic error page.
/// Lab requirement: show "Error processing the request" instead of a stack trace.
/// </summary>
public class ErrorController : Controller
{
    [Route("Error")]
    public IActionResult Index()
    {
        var feature = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
        // Only log; do NOT expose the message or stack trace to the user
        var path    = feature?.Path;
        var error   = feature?.Error;

        // In production you would log `error` to a logging system here.
        _ = path;
        _ = error;

        return View("Error");
    }
}
