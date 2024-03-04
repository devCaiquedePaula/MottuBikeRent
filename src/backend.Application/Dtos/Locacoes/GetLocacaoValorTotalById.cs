using BackEnd.Domain.Entities;
using BackEnd.Domain.Enum;
using HunterDomain.Extensions;
using MediatR;

namespace BackEnd.Application.Dtos.Locacoes;

public class GetLocacaoValorTotalById : IRequest<Locacao>
{
    private readonly Queries _query = new();

    public Guid Id { get; set; }

    public DateTime DataDevolucao{ get; set; }

    public string? Query { get => _query.GetQuery((int)QueryCQRS.QueryLocacaoById); }
}
