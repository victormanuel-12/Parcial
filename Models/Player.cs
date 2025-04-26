using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace MiAplicacionWeb.Models
{
  public class Player
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required(ErrorMessage = "El nombre es obligatorio")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "El nombre debe tener entre 2 y 100 caracteres")]
    [Display(Name = "Nombre del Jugador")]
    public string Name { get; set; }

    [Required(ErrorMessage = "La edad es obligatoria")]
    [Range(15, 50, ErrorMessage = "La edad debe estar entre 15 y 50 años")]
    [Display(Name = "Edad")]
    public int Age { get; set; }

    [Required(ErrorMessage = "La posición es obligatoria")]
    [StringLength(50, ErrorMessage = "La posición no puede exceder 50 caracteres")]
    [Display(Name = "Posición en el campo")]
    public string Position { get; set; }

    public ICollection<Assignment> Assignments { get; set; } = new List<Assignment>();
  }
}