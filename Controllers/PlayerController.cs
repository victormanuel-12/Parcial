
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiAplicacionWeb.Models;
using MiAplicacionWeb.Data; //
namespace MiAplicacionWeb.Controllers
{

  public class PlayerController : Controller
  {
    private readonly ApplicationDbContext _context;

    public PlayerController(ApplicationDbContext context)
    {
      _context = context;
    }

    // GET: Player
    public async Task<IActionResult> JugadoresConEquipos()
    {
      var jugadoresConEquipos = await _context.Players
          .Include(p => p.Assignments)
              .ThenInclude(a => a.Team)
          .Select(p => new JugadorEquipoViewModel
          {
            JugadorId = p.Id,
            NombreJugador = p.Name,
            Edad = p.Age,
            Posicion = p.Position,
            Equipos = p.Assignments.Select(a => a.Team.Name).ToList()
          })
          .ToListAsync();

      return View(jugadoresConEquipos);
    }

    // GET: Player/Create
    public IActionResult Create()
    {
      return View();
    }



    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Name,Age,Position,Equipo")] Player player)
    {
      if (ModelState.IsValid)
      {
        _context.Add(player);
        await _context.SaveChangesAsync();

        // Agregar mensaje de confirmación
        TempData["SuccessMessage"] = $"¡El jugador {player.Name} ha sido registrado exitosamente!";


      }
      return View(player);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
      return View("Error!");
    }
  }
}