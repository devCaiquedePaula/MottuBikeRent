using BackEnd.Domain.Entities;
using BackEnd.Domain.Enum;
using HunterDomain.Extensions;
using MediatR;

namespace BackEnd.Application.Dtos.Entregadores;

public class GetEntregadoresAll : IRequest<IEnumerable<Entregador>>
{
    private readonly Queries _query = new();

    public string? Query { get => _query.GetQuery((int)QueryCQRS.QueryEntregadoresAll); }
}
