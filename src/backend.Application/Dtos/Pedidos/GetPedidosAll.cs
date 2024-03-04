using BackEnd.Domain.Entities;
using BackEnd.Domain.Enum;
using HunterDomain.Extensions;
using MediatR;

namespace BackEnd.Application.Dtos.Pedidos;

public class GetPedidosAll : IRequest<IEnumerable<Pedido>>
{
    private readonly Queries _query = new();

    public string? Query { get => _query.GetQuery((int)QueryCQRS.QueryPedidosAll); }
}