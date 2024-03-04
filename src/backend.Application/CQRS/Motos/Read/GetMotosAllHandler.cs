using BackEnd.Application.Dtos.Motos;
using BackEnd.Domain.Entities;
using BackEnd.Domain.SeedWork;
using MediatR;

namespace BackEnd.Application.CQRS.Motos.Read;

public class GetMotosAllHandler : IRequestHandler<GetMotosAll, IEnumerable<Moto>>
{
    private readonly IRepositoryDapper _repositoryDapper;

    public GetMotosAllHandler(IRepositoryDapper repositoryDapper)
        => _repositoryDapper = repositoryDapper;

    public async Task<IEnumerable<Moto>> Handle(GetMotosAll request, CancellationToken cancellationToken)
    {
        var motosAll = await _repositoryDapper.GetAll<Moto>(request.Query!);
        return motosAll;
    }
}