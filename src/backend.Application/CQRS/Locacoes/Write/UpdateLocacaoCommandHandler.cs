using BackEnd.Application.Dtos.Locacoes;
using BackEnd.Domain.Entities;
using BackEnd.Domain.Enum;
using BackEnd.Domain.SeedWork;
using HunterDomain.Extensions;
using MediatR;

namespace BackEnd.Application.CQRS.Locacaos.Write;

public class UpdateLocacaoCommandHandler : IRequestHandler<UpdateLocacaoCommand, Locacao>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRepositoryDapper _repositoryDapper;
    private readonly Queries _query = new();

    public UpdateLocacaoCommandHandler(IUnitOfWork unitOfWork, IRepositoryDapper repositoryDapper)
    {
        _unitOfWork = unitOfWork;
        _repositoryDapper = repositoryDapper;
    }

    public async Task<Locacao> Handle(UpdateLocacaoCommand request, CancellationToken cancellationToken)
    {
        var daysPlanCustomer = request.PrazoEmDias;

        if (!(daysPlanCustomer == 7 || daysPlanCustomer == 15 || daysPlanCustomer == 30))
            return default!;

        switch (request.PrazoEmDias)
        {
            case 7:
                request.Plano = Planos._7dias.ToString().Replace("_", "");
                request.ValorDiaria = (int)Planos._7dias;
                break;
            case 15:
                request.Plano = Planos._15dias.ToString().Replace("_", "");
                request.ValorDiaria = (int)Planos._15dias;
                break;
            case 30:
                request.Plano = Planos._30dias.ToString().Replace("_", "");
                request.ValorDiaria = (int)Planos._30dias;
                break;
        }

        request.ValorTotal = request.ValorDiaria * request.PrazoEmDias;

        request.DataPrevistaTermino = DateTime.Now.AddDays(request.PrazoEmDias);


        var query = _query.GetQuery((int)QueryCQRS.QueryLocacaoById);
        var parameters = new
        {
            Id = request.Id
        };

        var locacaoUpdate = await _repositoryDapper.GetById<Locacao>(request.Id, query, parameters);

        if (locacaoUpdate is null)
            throw new Exception("Locacao is Null");

        locacaoUpdate.Update(locacaoUpdate.Plano, locacaoUpdate.PrazoEmDias, locacaoUpdate.DataCriacao,
            locacaoUpdate.DataInicio, locacaoUpdate.DataTermino, request.DataPrevistaTermino, locacaoUpdate.ValorDiaria, 
            request.ValorAdicional, request.ValorMulta, request.ValorTotal, request.Status, request.MotoId, request.EntregadorId
        );

        await _unitOfWork.Repository.UpdateObject<Locacao>(locacaoUpdate);

        await _unitOfWork.CommitAsync();

        return locacaoUpdate;
    }
}