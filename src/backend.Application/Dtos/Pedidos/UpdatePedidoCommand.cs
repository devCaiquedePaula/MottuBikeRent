using BackEnd.Domain.Entities;
using MediatR;

namespace BackEnd.Application.Dtos.Pedidos;

public class UpdatePedidoCommand : IRequest<Pedido>
{
    public Guid Id { get; set; }

    public Guid? EntregadorId { get; set; }

    public string? Status { get; set; }

}