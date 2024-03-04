using BackEnd.Domain.Entities;
using MediatR;

namespace BackEnd.Application.Dtos.Pedidos;

public class DeletePedidoCommand : IRequest<Pedido>
{
    public Guid Id { get; set; }
}