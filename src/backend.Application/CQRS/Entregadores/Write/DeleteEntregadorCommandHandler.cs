using BackEnd.Application.Dtos.Entregadores;
using BackEnd.Domain.Entities;
using BackEnd.Domain.Enum;
using BackEnd.Domain.SeedWork;
using HunterDomain.Extensions;
using MediatR;

namespace BackEnd.Application.CQRS.Entregadors.Write;

public class DeleteEntregadorCommandHandler : IRequestHandler<DeleteEntregadorCommand, Entregador>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRepositoryDapper _repositoryDapper;
    private readonly Queries _query = new();

    public DeleteEntregadorCommandHandler(IUnitOfWork unitOfWork, IRepositoryDapper repositoryDapper)
    {
        _unitOfWork = unitOfWork;
        _repositoryDapper = repositoryDapper;
    }

    public async Task<Entregador> Handle(DeleteEntregadorCommand request, CancellationToken cancellationToken)
    {
        var query = _query.GetQuery((int)QueryCQRS.QueryEntregadorById);
        var parameters = new
        {
            Id = request.Id
        };
        var EntregadorDelete = await _repositoryDapper.GetById<Entregador>(request.Id, query, parameters);

        if (EntregadorDelete is null)
            throw new Exception("Entregador is Null");


        await _unitOfWork.Repository.DeleteObject<Entregador>(EntregadorDelete);

        await _unitOfWork.CommitAsync();

        return EntregadorDelete;
    }
}