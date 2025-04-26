using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MiAplicacionWeb.Data;
using MiAplicacionWeb.Models;

namespace MiAplicacionWeb.Controllers
{
  public class AsignacionController : Controller
  {
    private readonly ApplicationDbContext _context;

    public AsignacionController(ApplicationDbContext context)
    {
      _context = context;
    }

    public IActionResult Create()
    {
      var players = _context.Players
          .Select(p => new
          {
            Id = p.Id,
            DisplayName = $"{p.Name} ({p.Position}) - Edad: {p.Age}"
          }).ToList();

      ViewBag.PlayersList = players; // Cambiado a PlayersList para coincidir con la vista
      ViewBag.Teams = new SelectList(_context.Teams, "Id", "Name");
      return View();
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("PlayerId,TeamId")] Assignment assignment)
    {

      bool assignmentExists = _context.Assignments
          .Any(a => a.PlayerId == assignment.PlayerId && a.TeamId == assignment.TeamId);

      if (assignmentExists)
      {
        TempData["WarningMessage"] = "Este jugador ya está asignado a este equipo.";
      }
      else
      {
        _context.Add(assignment);
        await _context.SaveChangesAsync();
        TempData["SuccessMessage"] = "Asignación creada correctamente.";
      }

      return RedirectToAction(nameof(Create));



    }


    public async Task<IActionResult> Index()
    {
      var assignments = await _context.Assignments
          .Include(a => a.Player)
          .Include(a => a.Team)
          .ToListAsync();
      return View(assignments);
    }
  }
}