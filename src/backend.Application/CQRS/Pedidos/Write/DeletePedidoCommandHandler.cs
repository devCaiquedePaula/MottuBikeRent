using BackEnd.Application.Dtos.Pedidos;
using BackEnd.Domain.Entities;
using BackEnd.Domain.Enum;
using BackEnd.Domain.SeedWork;
using HunterDomain.Extensions;
using MediatR;

namespace BackEnd.Application.CQRS.Pedidos.Write;

public class DeletePedidoCommandHandler : IRequestHandler<DeletePedidoCommand, Pedido>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRepositoryDapper _repositoryDapper;
    private readonly Queries _query = new();

    public DeletePedidoCommandHandler(IUnitOfWork unitOfWork, IRepositoryDapper repositoryDapper)
    {
        _unitOfWork = unitOfWork;
        _repositoryDapper = repositoryDapper;
    }

    public async Task<Pedido> Handle(DeletePedidoCommand request, CancellationToken cancellationToken)
    {
        var query = _query.GetQuery((int)QueryCQRS.QueryPedidoById);
        var parameters = new
        {
            Id = request.Id
        };
        var PedidoDelete = await _repositoryDapper.GetById<Pedido>(request.Id, query, parameters);

        if (PedidoDelete is null)
            throw new Exception("Pedido is Null");


        await _unitOfWork.Repository.DeleteObject<Pedido>(PedidoDelete);

        await _unitOfWork.CommitAsync();

        return PedidoDelete;
    }
}