using BackEnd.Application.Dtos.Entregadores;
using BackEnd.Domain.Entities;
using BackEnd.Domain.SeedWork;
using MediatR;

namespace BackEnd.Application.CQRS.Entregadors.Read;

public class GetEntregadorByIdHandler : IRequestHandler<GetEntregadorById, Entregador>
{
    private readonly IRepositoryDapper _repositoryDapper;

    public GetEntregadorByIdHandler(IRepositoryDapper repositoryDapper)
        => _repositoryDapper = repositoryDapper;
    

    public async Task<Entregador> Handle(GetEntregadorById request, CancellationToken cancellationToken)
    {
        var parameters = new
        {
            NumeroCNH = request.NumeroCNH
        };

        var entregadorsByPlaca = await _repositoryDapper.GetById<Entregador>(request.Id,request.Query!, parameters);

        return entregadorsByPlaca;
    }
}
