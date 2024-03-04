using BackEnd.Application.Dtos.Locacoes;
using BackEnd.Domain.Entities;
using BackEnd.Domain.SeedWork;
using MediatR;

namespace BackEnd.Application.CQRS.Locacaos.Read;

public class GetLocacaoByIdHandler : IRequestHandler<GetLocacaoById, Locacao>
{
    private readonly IRepositoryDapper _repositoryDapper;

    public GetLocacaoByIdHandler(IRepositoryDapper repositoryDapper)
        => _repositoryDapper = repositoryDapper;
    

    public async Task<Locacao> Handle(GetLocacaoById request, CancellationToken cancellationToken)
    {
        var parameters = new
        {
            Id = request.Id
        };

        var LocacaosByPlaca = await _repositoryDapper.GetById<Locacao>(request.Id,request.Query!, parameters);

        return LocacaosByPlaca;
    }
}
