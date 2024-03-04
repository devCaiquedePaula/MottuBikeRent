using BackEnd.Domain.Entities;
using BackEnd.Domain.Enum;
using HunterDomain.Extensions;
using MediatR;

namespace BackEnd.Application.Dtos.Pedidos;

public class GetPedidoById : IRequest<Pedido>
{
    private readonly Queries _query = new();

    public Guid Id { get; set; }

    public string? Query { get => _query.GetQuery((int)QueryCQRS.QueryPedidoById); }
}