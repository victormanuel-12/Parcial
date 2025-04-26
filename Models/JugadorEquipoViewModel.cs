using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MiAplicacionWeb.Models
{
  public class JugadorEquipoViewModel
  {
    public int JugadorId { get; set; }
    public string? NombreJugador { get; set; }
    public int Edad { get; set; }
    public string? Posicion { get; set; }
    public List<string>? Equipos { get; set; }
  }

}