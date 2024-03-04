using BackEnd.Domain.Entities;
using BackEnd.Infra.Data.EF.EntityConfiguration;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Infra.Data.EF.Context;

public class PgDbContext : DbContext
{
    public PgDbContext(DbContextOptions<PgDbContext> options) : base(options)
    {}

    public DbSet<Moto> Motos { get; set; }
    public DbSet<Entregador> Entregadores { get; set; }
    public DbSet<Locacao> Locacoes { get; set; }
    public DbSet<Pedido> Pedidos { get; set; }
    public DbSet<Notificacao> notificacoes { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfiguration(new MotosConfiguration());
        builder.ApplyConfiguration(new EntregadoresConfiguration());
        builder.ApplyConfiguration(new LocacoesConfiguration());
        builder.ApplyConfiguration(new PedidosConfiguration());
        builder.ApplyConfiguration(new NotificacoesConfiguration());
    }

}