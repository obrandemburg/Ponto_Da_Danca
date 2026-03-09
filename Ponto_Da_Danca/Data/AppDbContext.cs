// 4. Data/AppDbContext.cs
// O contexto do banco aplica as configurações isoladas.
using Microsoft.EntityFrameworkCore;
using PontoDaDanca.Entities;
using System.Reflection.Emit;

namespace PontoDaDanca.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Usuario> Usuarios { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Aplica todas as classes de 'Configurations' automaticamente
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}