using BackEnd.Application.Dtos.Locacoes;
using BackEnd.Domain.Entities;
using BackEnd.Domain.SeedWork;
using MediatR;

namespace BackEnd.Application.CQRS.Motos.Read;

public class GetLocacoesAllHandler : IRequestHandler<GetLocacoesAll, IEnumerable<Locacao>>
{
    private readonly IRepositoryDapper _repositoryDapper;

    public GetLocacoesAllHandler(IRepositoryDapper repositoryDapper)
    => _repositoryDapper = repositoryDapper;

    public async Task<IEnumerable<Locacao>> Handle(GetLocacoesAll request, CancellationToken cancellationToken)
    {
        var motosAll = await _repositoryDapper.GetAll<Locacao>(request.Query!);
        return motosAll;
    }
}