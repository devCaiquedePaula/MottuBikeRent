using BackEnd.Application.Dtos.Motos;
using BackEnd.Domain.Entities;
using BackEnd.Domain.SeedWork;
using MediatR;

namespace BackEnd.Application.CQRS.Motos.Read;

public class GetMotoByIdHandler : IRequestHandler<GetMotoById, Moto>
{
    private readonly IRepositoryDapper _repositoryDapper;

    public GetMotoByIdHandler(IRepositoryDapper repositoryDapper)
        => _repositoryDapper = repositoryDapper;
    

    public async Task<Moto> Handle(GetMotoById request, CancellationToken cancellationToken)
    {
        var parameters = new
        {
            Placa = request.Placa
        };

        var motosByPlaca = await _repositoryDapper.GetById<Moto>(request.Id,request.Query!, parameters);

        return motosByPlaca;
    }
}
