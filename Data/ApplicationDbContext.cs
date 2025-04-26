using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MiAplicacionWeb.Models;
using System.ComponentModel.DataAnnotations.Schema;
namespace MiAplicacionWeb.Data;

public class ApplicationDbContext : IdentityDbContext
{
  public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
      : base(options)
  {
  }


  public DbSet<Player> Players => Set<Player>();
  public DbSet<Team> Teams => Set<Team>();
  public DbSet<Assignment> Assignments => Set<Assignment>();

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    base.OnModelCreating(modelBuilder);

    // Restricción: un jugador no puede estar dos veces en el mismo equipo
    modelBuilder.Entity<Assignment>()
        .HasIndex(a => new { a.PlayerId, a.TeamId })
        .IsUnique();

    // Relaciones
    modelBuilder.Entity<Assignment>()
        .HasOne(a => a.Player)
        .WithMany(p => p.Assignments)
        .HasForeignKey(a => a.PlayerId)
        .OnDelete(DeleteBehavior.Cascade);

    modelBuilder.Entity<Assignment>()
        .HasOne(a => a.Team)
        .WithMany(t => t.Assignments)
        .HasForeignKey(a => a.TeamId)
        .OnDelete(DeleteBehavior.Cascade);
  }
}
