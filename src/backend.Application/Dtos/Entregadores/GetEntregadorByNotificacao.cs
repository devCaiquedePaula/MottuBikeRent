using BackEnd.Domain.Entities;
using BackEnd.Domain.Enum;
using HunterDomain.Extensions;
using MediatR;

namespace BackEnd.Application.Dtos.Entregadores;

public class GetEntregadorByNotificacao : IRequest<IEnumerable<Entregador>>
{
    private readonly Queries _query = new();

    public Guid PedidoId { get; set; }

    public string? Query { get => _query.GetQuery((int)QueryCQRS.QueryEntregadorByNotificacao); }
}
