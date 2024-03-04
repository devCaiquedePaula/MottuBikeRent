using BackEnd.Application.Dtos.Pedidos;
using BackEnd.Domain.Entities;
using BackEnd.Domain.SeedWork;
using MediatR;

namespace BackEnd.Application.CQRS.Pedidos.Read;

public class GetPedidoByIdHandler : IRequestHandler<GetPedidoById, Pedido>
{
    private readonly IRepositoryDapper _repositoryDapper;

    public GetPedidoByIdHandler(IRepositoryDapper repositoryDapper)
        => _repositoryDapper = repositoryDapper;
    

    public async Task<Pedido> Handle(GetPedidoById request, CancellationToken cancellationToken)
    {
        var parameters = new
        {
            Id = request.Id
        };

        var PedidosByPlaca = await _repositoryDapper.GetById<Pedido>(request.Id,request.Query!, parameters);

        return PedidosByPlaca;
    }
}
