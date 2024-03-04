using BackEnd.Domain.Entities;

namespace BackEnd.Application.Dtos.Pedidos;

public class PedidosNotificadosByEntregadorId
{
	public Guid PedidoId { get; set; }

	public List<Guid>? ListaEntregadores { get; set; }
	
}
