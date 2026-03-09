// 3. Configurations/UsuarioConfiguration.cs
// Configura a tabela no banco de forma limpa, isolada do DbContext.
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ponto_Da_Danca.Entities;

namespace Ponto_Da_Danca.Configurations;

public class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
{
    public void Configure(EntityTypeBuilder<Usuario> builder)
    {
        builder.HasKey(u => u.Id);
        builder.Property(u => u.Nome).IsRequired().HasMaxLength(150);
        builder.HasIndex(u => u.Email).IsUnique();
    }
}