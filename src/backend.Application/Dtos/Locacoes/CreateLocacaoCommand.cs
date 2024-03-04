using BackEnd.Domain.Entities;
using MediatR;

namespace BackEnd.Application.Dtos.Locacoes;

public class CreateLocacaoCommand : IRequest<Locacao>
{
    public int PrazoEmDias { get; set; }

    public DateTime DataCriacao { get; set; }

    public Guid? MotoId { get; set; }

    public Guid? EntregadorId { get; set; }
}