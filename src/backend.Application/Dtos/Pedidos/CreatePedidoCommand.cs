using BackEnd.Domain.Entities;
using MediatR;

namespace BackEnd.Application.Dtos.Pedidos;

public class CreatePedidoCommand : IRequest<Pedido>
{
	public decimal ValorDaCorrida { get; set; }
}