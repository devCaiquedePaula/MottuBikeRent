using BackEnd.Application.Dtos.Pedidos;
using BackEnd.Domain.Entities;
using BackEnd.Domain.Enum;
using BackEnd.Domain.SeedWork;
using HunterDomain.Extensions;
using MediatR;

namespace BackEnd.Application.CQRS.Pedidos.Write;

public class UpdatePedidoCommandHandler : IRequestHandler<UpdatePedidoCommand, Pedido>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRepositoryDapper _repositoryDapper;
    private readonly Queries _query = new();

    public UpdatePedidoCommandHandler(IUnitOfWork unitOfWork, IRepositoryDapper repositoryDapper)
    {
        _unitOfWork = unitOfWork;
        _repositoryDapper = repositoryDapper;
    }

    public async Task<Pedido> Handle(UpdatePedidoCommand request, CancellationToken cancellationToken)
    {

        var queryThereNotificationByEntregador = _query.GetQuery((int)QueryCQRS.QueryNotificaoByEntregadorPedido);

        var parametersNotication = new
        {
            PedidoId = request.Id,
            EntregadorId = request.EntregadorId,
        };


        var thereNotificationByEntregador = await _repositoryDapper.GetById<Notificacao>(request.Id, queryThereNotificationByEntregador, parametersNotication);

        if (thereNotificationByEntregador is null)
            return default!;

        var query = _query.GetQuery((int)QueryCQRS.QueryPedidoById);
        var parameters = new
        {
            Id = request.Id
        };

        var pedidoUpdate = await _repositoryDapper.GetById<Pedido>(request.Id, query, parameters);

        if (pedidoUpdate is null)
            throw new Exception("Pedido is Null");

        if (request.Status!.Equals(StatusPedido.Aceito.ToString()))
        {
            if (pedidoUpdate.Status! != StatusPedido.Disponivel.ToString())
                return default!;

            pedidoUpdate.Update(pedidoUpdate.DataCriacao, StatusPedido.Aceito.ToString(), pedidoUpdate.ValorDaCorrida, request.EntregadorId);
        }
        else if (request.Status!.Equals(StatusPedido.Entregue.ToString())) {

            if (pedidoUpdate.Status! != StatusPedido.Aceito.ToString())
                return default!;

            pedidoUpdate.Update(pedidoUpdate.DataCriacao, StatusPedido.Entregue.ToString(), pedidoUpdate.ValorDaCorrida, request.EntregadorId);
        }

        await _unitOfWork.Repository.UpdateObject<Pedido>(pedidoUpdate);

        await _unitOfWork.CommitAsync();

        return pedidoUpdate;
    }
}