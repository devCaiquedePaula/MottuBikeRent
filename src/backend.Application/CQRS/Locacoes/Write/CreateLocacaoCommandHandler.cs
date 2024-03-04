using BackEnd.Application.Dtos.Locacoes;
using BackEnd.Domain.Entities;
using BackEnd.Domain.Enum;
using BackEnd.Domain.SeedWork;
using HunterDomain.Extensions;
using MediatR;

namespace BackEnd.Application.CQRS.Locacaos.Write;

public class CreateLocacaoCommandHandler : IRequestHandler<CreateLocacaoCommand, Locacao>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRepositoryDapper _repositoryDapper;
    private readonly Queries _query = new();

    public CreateLocacaoCommandHandler(IUnitOfWork unitOfWork, IRepositoryDapper repositoryDapper)
    {
        _unitOfWork = unitOfWork;
        _repositoryDapper = repositoryDapper;
    }

    public async Task<Locacao> Handle(CreateLocacaoCommand request, CancellationToken cancellationToken)
    {
        var daysPlanCustomer = request.PrazoEmDias;
        var namePlanCustomer = string.Empty;
        var valueForDay = 0;
        var queryEntregador = _query.GetQuery((int)QueryCQRS.QueryEntregadorById);
        var queryMotoDisponivel = _query.GetQuery((int)QueryCQRS.QueryMotoDisponivelById);

        var parameters = new
        {
            Id = request.EntregadorId
        };

        var entregadorById = await _repositoryDapper.GetById<Entregador>((Guid)request.EntregadorId!, queryEntregador, parameters);

        if (!entregadorById.CategoriaCNH!.Contains("A"))
            return default!;

        parameters = new
        {
            Id = request.MotoId
        };

        var motoById = await _repositoryDapper.GetById<long>((Guid)request.MotoId!, queryMotoDisponivel, parameters);

        if (motoById > 0)
            return default!;

        if (!(daysPlanCustomer == 7 || daysPlanCustomer == 15 || daysPlanCustomer == 30))
            return default!;

        switch (request.PrazoEmDias) {
            case 7:
                namePlanCustomer = Planos._7dias.ToString().Replace("_", "");
                valueForDay = (int)Planos._7dias;
                break;
            case 15:
                namePlanCustomer = Planos._15dias.ToString().Replace("_", "");
                valueForDay = (int)Planos._15dias;
                break;
            case 30:
                namePlanCustomer = Planos._30dias.ToString().Replace("_", "");
                valueForDay = (int)Planos._30dias;
                break;
        }

        var valueTotal = valueForDay * request.PrazoEmDias;

        if (DateTime.Compare(request.DataCriacao, DateTime.Now) < 0)
            return default!;

        var dateCreated = request.DataCriacao;
        var dateStart = request.DataCriacao.AddDays(1);
        var dateEnd = request.DataCriacao.AddDays(request.PrazoEmDias);
        var datePrevEnd = request.DataCriacao.AddDays(request.PrazoEmDias);
        var status = StatusLocacao.Ativa.ToString();

        var newLocacao = new Locacao(namePlanCustomer, request.PrazoEmDias, dateCreated,
            dateStart, dateEnd, datePrevEnd, valueForDay, 
            null, null, valueTotal, status, request.MotoId, request.EntregadorId
        );

        await _unitOfWork.Repository.AddObject<Locacao>(newLocacao);

        await _unitOfWork.CommitAsync();

        return newLocacao;
    }
}
