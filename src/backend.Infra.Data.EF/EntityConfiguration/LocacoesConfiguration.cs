using BackEnd.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BackEnd.Infra.Data.EF.EntityConfiguration;

public class LocacoesConfiguration : IEntityTypeConfiguration<Locacao>
{
    public void Configure(EntityTypeBuilder<Locacao> builder)
    {
        builder.HasKey(m => m.Id);
        builder.Property(m => m.Plano).HasMaxLength(20).IsRequired();
        builder.Property(m => m.PrazoEmDias).IsRequired().HasAnnotation("Range", new[] { 1, int.MaxValue });
        builder.Property(m => m.DataCriacao).IsRequired().HasColumnType("date");
        builder.Property(m => m.DataInicio).IsRequired().HasColumnType("date");
        builder.Property(m => m.DataTermino).IsRequired().HasColumnType("date");
        builder.Property(m => m.DataPrevistaTermino).IsRequired().HasColumnType("date");
        builder.Property(m => m.ValorDiaria).IsRequired().HasAnnotation("Range", new[] { 1, int.MaxValue });
        builder.Property(m => m.ValorAdicional);
        builder.Property(m => m.ValorMulta);
        builder.Property(m => m.ValorTotal).IsRequired().HasAnnotation("Range", new[] { 1, int.MaxValue });
        builder.Property(m => m.Status).HasMaxLength(10).IsRequired();

        builder.HasMany(m => m.Motos)
                .WithOne(m => m.Locacao)
                .HasForeignKey(m => m.LocacaoId)
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired(false);

        builder.HasMany(m => m.Entregadores)
                .WithOne(m => m.Locacao)
                .HasForeignKey(m => m.LocacaoId)
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired(false);

        builder.HasData(
            new Locacao("7Dias", 7 , DateTime.Now, DateTime.Now.AddDays(1), DateTime.Now.AddDays(7), DateTime.Now.AddDays(7),
                30, null, null , 30*7, "Ativo", null, null)
        );
    }
}
