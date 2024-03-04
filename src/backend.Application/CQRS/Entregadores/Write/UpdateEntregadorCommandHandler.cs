using BackEnd.Application.Dtos.Entregadores;
using BackEnd.Domain.Entities;
using BackEnd.Domain.Enum;
using BackEnd.Domain.SeedWork;
using HunterDomain.Extensions;
using MediatR;

namespace BackEnd.Application.CQRS.Entregadors.Write;

public class UpdateEntregadorCommandHandler : IRequestHandler<UpdateEntregadorCommand, Entregador>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRepositoryDapper _repositoryDapper;
    private readonly Queries _query = new();

    public UpdateEntregadorCommandHandler(IUnitOfWork unitOfWork, IRepositoryDapper repositoryDapper)
    {
        _unitOfWork = unitOfWork;
        _repositoryDapper = repositoryDapper;
    }

    public async Task<Entregador> Handle(UpdateEntregadorCommand request, CancellationToken cancellationToken)
    {
        var query = _query.GetQuery((int)QueryCQRS.QueryEntregadorById);
        var parameters = new
        {
            Id = request.Id
        };
        var EntregadorUpdate = await _repositoryDapper.GetById<Entregador>(request.Id, query, parameters);

        var categoryCNH = request.CategoriaCNH!.Trim();

        var isValidCategoryCNH = (categoryCNH.Equals("A") || categoryCNH.Equals("B") || categoryCNH.Equals("AB"));

        if (!isValidCategoryCNH)
        {
            EntregadorUpdate.CategoriaCNH = "I";
            return EntregadorUpdate;
        }

        if (EntregadorUpdate is null)
            return default!;

        EntregadorUpdate.Update(request.Nome, request.CNH, request.CategoriaCNH, request.CNPJ, request.DataNascimento,
            request.NumeroCNH!, true);

        await _unitOfWork.Repository.UpdateObject<Entregador>(EntregadorUpdate);

        await _unitOfWork.CommitAsync();

        return EntregadorUpdate;
    }
}