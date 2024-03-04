using BackEnd.Application.Dtos.Entregadores;
using BackEnd.Domain.Entities;
using BackEnd.Domain.SeedWork;
using MediatR;

namespace BackEnd.Application.CQRS.Entregadors.Read;

public class GetEntregadorByNotificacaoHandler : IRequestHandler<GetEntregadorByNotificacao, IEnumerable<Entregador>>
{
    private readonly IRepositoryDapper _repositoryDapper;

    public GetEntregadorByNotificacaoHandler(IRepositoryDapper repositoryDapper)
        => _repositoryDapper = repositoryDapper;
    

    public async Task<IEnumerable<Entregador>> Handle(GetEntregadorByNotificacao request, CancellationToken cancellationToken)
    {
        var parameters = new
        {
            PedidoId = request.PedidoId
        };

        var entregadorsByPlaca = await _repositoryDapper.GetByIdList<Entregador>(request.PedidoId,request.Query!, parameters);

        return entregadorsByPlaca;
    }
}
