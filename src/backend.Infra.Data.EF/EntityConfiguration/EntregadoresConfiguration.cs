using BackEnd.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BackEnd.Infra.Data.EF.EntityConfiguration;

public class EntregadoresConfiguration : IEntityTypeConfiguration<Entregador>
{
    public void Configure(EntityTypeBuilder<Entregador> builder)
    {
        builder.HasKey(m => m.Id);
        builder.Property(m => m.CNH).HasMaxLength(250).IsRequired();
        builder.Property(m => m.NumeroCNH).HasMaxLength(20).IsRequired();
        builder.Property(m => m.CategoriaCNH).HasMaxLength(2).IsRequired();
        builder.Property(m => m.DataNascimento).IsRequired().HasColumnType("date");

        builder.Property(m => m.Ativo).IsRequired();

        builder.HasIndex(m => m.NumeroCNH).IsUnique();
        builder.HasIndex(m => m.CNPJ).IsUnique();

        builder.HasData(
            new Entregador("José Antonio", "https://conteudo.imguol.com.br/c/entretenimento/ae/2022/06/03/nova-cnh-2022-1654284075548_v2_900x506.jpg.webp",
                "AB", "10.029.0190/0001-90", new DateTime(1978,5,16), "92385235" , true
            ),
            new Entregador("Pedro Antonio", "https://conteudo.imguol.com.br/c/entretenimento/ae/2022/06/03/nova-cnh-2022-1654284075548_v2_900x506.jpg.webp",
                "AB", "10.029.0190/0001-89", new DateTime(1978,5,16), "92385234" , true
            ),
            new Entregador("Maria José", "https://conteudo.imguol.com.br/c/entretenimento/ae/2022/06/03/nova-cnh-2022-1654284075548_v2_900x506.jpg.webp",
                "AB", "10.029.0190/0001-91", new DateTime(1978,5,16), "92385233" , true
            ),
            new Entregador("Antonio Pedro", "https://conteudo.imguol.com.br/c/entretenimento/ae/2022/06/03/nova-cnh-2022-1654284075548_v2_900x506.jpg.webp",
                "AB", "10.029.0190/0001-92", new DateTime(1978,5,16), "92385232" , true
            )
        );
    }
}
