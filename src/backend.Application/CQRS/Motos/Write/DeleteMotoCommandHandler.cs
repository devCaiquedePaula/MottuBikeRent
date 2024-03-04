using BackEnd.Application.Dtos.Motos;
using BackEnd.Domain.Entities;
using BackEnd.Domain.Enum;
using BackEnd.Domain.SeedWork;
using HunterDomain.Extensions;
using MediatR;

namespace BackEnd.Application.CQRS.Motos.Write;

public class DeleteMotoCommandHandler : IRequestHandler<DeleteMotoCommand, Moto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRepositoryDapper _repositoryDapper;
    private readonly Queries _query = new();

    public DeleteMotoCommandHandler(IUnitOfWork unitOfWork, IRepositoryDapper repositoryDapper)
    {
        _unitOfWork = unitOfWork;
        _repositoryDapper = repositoryDapper;
    }

    public async Task<Moto> Handle(DeleteMotoCommand request, CancellationToken cancellationToken)
    {
        var parameters = new
        {
            Id = request.Id
        };

        var queryValidDelete = _query.GetQuery((int)QueryCQRS.QueryMotoPossuiLocacoes);

        var isValideDelete = await _repositoryDapper.GetById<long>(request.Id, queryValidDelete, parameters);

        if (isValideDelete > 0)
            return default!;

        var query = _query.GetQuery((int)QueryCQRS.QueryMotosById);

        var motoDelete = await _repositoryDapper.GetById<Moto>(request.Id, query, parameters);

        if (motoDelete is null)
            throw new Exception("Moto is Null");


        await _unitOfWork.Repository.DeleteObject<Moto>(motoDelete);

        await _unitOfWork.CommitAsync();

        return motoDelete;
    }
}