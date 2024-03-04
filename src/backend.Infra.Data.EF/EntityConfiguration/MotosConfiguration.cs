using BackEnd.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BackEnd.Infra.Data.EF.EntityConfiguration;

public class MotosConfiguration : IEntityTypeConfiguration<Moto>
{
    public void Configure(EntityTypeBuilder<Moto> builder)
    {
        builder.HasKey(m => m.Id);
        builder.Property(m => m.Modelo).HasMaxLength(50).IsRequired();
        builder.Property(m => m.Placa).HasMaxLength(10).IsRequired();
        builder.Property(m => m.Ativo).IsRequired();

        builder.HasIndex(m => m.Placa).IsUnique();

        builder.HasData(
            new Moto(2014, "XRE", "RVG-0J83", true),
            new Moto(2015, "CG 160 FAN", "RVG-0J84", true),
            new Moto(2008, "CG 150 START", "RVG-0J82", true),
            new Moto(2021, "XRE", "RVG-0J81", true),
            new Moto(2019, "CG 150 TITAN", "RVG-0J80", true)
        );
    }
}
