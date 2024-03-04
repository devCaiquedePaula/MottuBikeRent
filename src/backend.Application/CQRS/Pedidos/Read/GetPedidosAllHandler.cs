using BackEnd.Application.Dtos.Pedidos;
using BackEnd.Domain.Entities;
using BackEnd.Domain.SeedWork;
using MediatR;

namespace BackEnd.Application.CQRS.Motos.Read;

public class GetPedidoesAllHandler : IRequestHandler<GetPedidosAll, IEnumerable<Pedido>>
{
    private readonly IRepositoryDapper _repositoryDapper;

    public GetPedidoesAllHandler(IRepositoryDapper repositoryDapper)
    => _repositoryDapper = repositoryDapper;

    public async Task<IEnumerable<Pedido>> Handle(GetPedidosAll request, CancellationToken cancellationToken)
    {
        var motosAll = await _repositoryDapper.GetAll<Pedido>(request.Query!);
        return motosAll;
    }
}