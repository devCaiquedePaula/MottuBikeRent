using BackEnd.Application.Dtos.Entregadores;
using BackEnd.Domain.Entities;
using BackEnd.Domain.Enum;
using BackEnd.Domain.SeedWork;
using HunterDomain.Extensions;
using MediatR;

namespace BackEnd.Application.CQRS.Entregadors.Write;

public class UpdateEntregadorCNHCommandHandler : IRequestHandler<UpdateEntregadorCNHCommand, Entregador>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRepositoryDapper _repositoryDapper;
    private readonly Queries _query = new();

    public UpdateEntregadorCNHCommandHandler(IUnitOfWork unitOfWork, IRepositoryDapper repositoryDapper)
    {
        _unitOfWork = unitOfWork;
        _repositoryDapper = repositoryDapper;
    }

    public async Task<Entregador> Handle(UpdateEntregadorCNHCommand request, CancellationToken cancellationToken)
    {
        var query = _query.GetQuery((int)QueryCQRS.QueryEntregadorById);
        var parameters = new
        {
            Id = request.Id
        };
        var entregadorUpdate = await _repositoryDapper.GetById<Entregador>(request.Id, query, parameters);

        if (entregadorUpdate is null)
            throw new Exception("Entregador is Null");

        entregadorUpdate.Update(entregadorUpdate.Nome, request.CNH, entregadorUpdate.CategoriaCNH, entregadorUpdate.CNPJ, entregadorUpdate.DataNascimento,
            entregadorUpdate.NumeroCNH!, true);

        await _unitOfWork.Repository.UpdateObject<Entregador>(entregadorUpdate);

        await _unitOfWork.CommitAsync();

        return entregadorUpdate;
    }
}