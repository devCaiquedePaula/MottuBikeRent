using BackEnd.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BackEnd.Infra.Data.EF.EntityConfiguration;

public class NotificacoesConfiguration : IEntityTypeConfiguration<Notificacao>
{
    public void Configure(EntityTypeBuilder<Notificacao> builder)
    {
        builder.HasKey(m => m.Id);
        builder.Property(m => m.EntregadorId).IsRequired();
        builder.Property(m => m.PedidoId).IsRequired();
        builder.Property(m => m.DataNoticacao).IsRequired().HasColumnType("date");
    }
}
