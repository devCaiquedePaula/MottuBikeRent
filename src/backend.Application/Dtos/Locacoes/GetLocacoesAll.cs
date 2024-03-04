using BackEnd.Domain.Entities;
using BackEnd.Domain.Enum;
using HunterDomain.Extensions;
using MediatR;

namespace BackEnd.Application.Dtos.Locacoes;

public class GetLocacoesAll : IRequest<IEnumerable<Locacao>>
{
    private readonly Queries _query = new();

    public string? Query { get => _query.GetQuery((int)QueryCQRS.QueryLocacoesAll); }
}
