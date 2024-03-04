using BackEnd.Application.Dtos.Locacoes;
using BackEnd.Domain.Entities;
using BackEnd.Domain.Enum;
using BackEnd.Domain.SeedWork;
using HunterDomain.Extensions;
using MediatR;

namespace BackEnd.Application.CQRS.Locacaos.Write;

public class DeleteLocacaoCommandHandler : IRequestHandler<DeleteLocacaoCommand, Locacao>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRepositoryDapper _repositoryDapper;
    private readonly Queries _query = new();

    public DeleteLocacaoCommandHandler(IUnitOfWork unitOfWork, IRepositoryDapper repositoryDapper)
    {
        _unitOfWork = unitOfWork;
        _repositoryDapper = repositoryDapper;
    }

    public async Task<Locacao> Handle(DeleteLocacaoCommand request, CancellationToken cancellationToken)
    {
        var query = _query.GetQuery((int)QueryCQRS.QueryLocacaoById);
        var parameters = new
        {
            Id = request.Id
        };
        var LocacaoDelete = await _repositoryDapper.GetById<Locacao>(request.Id, query, parameters);

        if (LocacaoDelete is null)
            throw new Exception("Locacao is Null");


        await _unitOfWork.Repository.DeleteObject<Locacao>(LocacaoDelete);

        await _unitOfWork.CommitAsync();

        return LocacaoDelete;
    }
}