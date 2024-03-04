using BackEnd.Application.Dtos.Pedidos;
using BackEnd.Domain.Entities;
using BackEnd.Domain.Enum;
using BackEnd.Domain.SeedWork;
using HunterDomain.Extensions;
using MediatR;

namespace BackEnd.Application.CQRS.Pedidos.Write;

public class CreatePedidoCommandHandler : IRequestHandler<CreatePedidoCommand, Pedido>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRabbitMQMessageRepository _rabbitMQ;
    private readonly IRepositoryDapper _repositoryDapper;
    private const string QUEUE_NAME = "notification-order";
    private readonly Queries _query = new();

    public CreatePedidoCommandHandler(IUnitOfWork unitOfWork, IRabbitMQMessageRepository rabbitMQ, IRepositoryDapper repositoryDapper)
    {
        _unitOfWork = unitOfWork;
        _rabbitMQ = rabbitMQ;
        _repositoryDapper = repositoryDapper;
    }

    public async Task<Pedido> Handle(CreatePedidoCommand request, CancellationToken cancellationToken)
    {
        var newPedido = new Pedido(DateTime.Now, StatusPedido.Disponivel.ToString(), request.ValorDaCorrida, null);

        var entregadoresDisponiveis = await _repositoryDapper.GetAll<Entregador>(_query.GetQuery((int)QueryCQRS.QueryEntregadoresDisponives));

        await _unitOfWork.Repository.AddObject<Pedido>(newPedido!);

        await _unitOfWork.CommitAsync();

        var messagePedido = new PedidosNotificadosByEntregadorId() {
            PedidoId = newPedido.Id,
            ListaEntregadores = entregadoresDisponiveis.Select(id => id.Id).ToList()
        };

        await _rabbitMQ.PublishMessage<PedidosNotificadosByEntregadorId>(QUEUE_NAME, messagePedido);

        return newPedido;
    }
}
