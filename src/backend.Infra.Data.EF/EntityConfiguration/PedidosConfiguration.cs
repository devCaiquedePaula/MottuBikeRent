using BackEnd.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BackEnd.Infra.Data.EF.EntityConfiguration;

public class PedidosConfiguration : IEntityTypeConfiguration<Pedido>
{
    public void Configure(EntityTypeBuilder<Pedido> builder)
    {
        builder.HasKey(m => m.Id);
        builder.Property(m => m.Status).HasMaxLength(20).IsRequired();
        builder.Property(m => m.DataCriacao).IsRequired().HasColumnType("date");
        builder.Property(m => m.ValorDaCorrida).IsRequired().HasAnnotation("Range", new[] { 1, int.MaxValue });

        builder.HasData(
            new Pedido(DateTime.Now, "Disponível", 87, null)
        );
    }
}
