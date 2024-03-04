using BackEnd.Application.Dtos.Entregadores;
using BackEnd.Domain.Entities;
using BackEnd.Domain.SeedWork;
using MediatR;

namespace BackEnd.Application.CQRS.Motos.Read;

public class GetEntregadoresAllHandler : IRequestHandler<GetEntregadoresAll, IEnumerable<Entregador>>
{
    private readonly IRepositoryDapper _repositoryDapper;

    public GetEntregadoresAllHandler(IRepositoryDapper repositoryDapper)
    => _repositoryDapper = repositoryDapper;

    public async Task<IEnumerable<Entregador>> Handle(GetEntregadoresAll request, CancellationToken cancellationToken)
    {
        var motosAll = await _repositoryDapper.GetAll<Entregador>(request.Query!);
        return motosAll;
    }
}