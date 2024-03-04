using BackEnd.Domain.Entities;
using MediatR;

namespace BackEnd.Application.Dtos.Locacoes;

public class DeleteLocacaoCommand : IRequest<Locacao>
{
    public Guid Id { get; set; }
}