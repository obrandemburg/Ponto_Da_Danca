using Microsoft.EntityFrameworkCore;
using Ponto_Da_Danca.Entities;
using System.Reflection.Emit;

namespace Ponto_Da_Danca.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Ritmo> Ritmos { get; set; } // Nova tabela
    public DbSet<Turma> Turmas { get; set; } // Nova tabela

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}