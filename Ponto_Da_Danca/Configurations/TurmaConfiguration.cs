using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ponto_Da_Danca.Entities;

namespace Ponto_Da_Danca.Configuratios;

public class TurmaConfiguration : IEntityTypeConfiguration<Turma>
{
    public void Configure(EntityTypeBuilder<Turma> builder)
    {
        builder.HasKey(t => t.Id);
        builder.Property(t => t.Sala).IsRequired().HasMaxLength(50);
        builder.Property(t => t.NivelTecnico).IsRequired().HasMaxLength(50);

        // Relacionamento 1:N - Um Ritmo tem várias Turmas
        builder.HasOne(t => t.Ritmo)
               .WithMany()
               .HasForeignKey(t => t.RitmoId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}