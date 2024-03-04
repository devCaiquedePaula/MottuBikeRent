using BackEnd.Application.Dtos.Locacoes;
using BackEnd.Domain.Entities;
using BackEnd.Domain.Enum;
using BackEnd.Domain.SeedWork;
using MediatR;

namespace BackEnd.Application.CQRS.Locacaos.Read;

public class GetLocacaoValorTotalByIdHandler : IRequestHandler<GetLocacaoValorTotalById, Locacao>
{
    private readonly IRepositoryDapper _repositoryDapper;

    public GetLocacaoValorTotalByIdHandler(IRepositoryDapper repositoryDapper)
        => _repositoryDapper = repositoryDapper;
    

    public async Task<Locacao> Handle(GetLocacaoValorTotalById request, CancellationToken cancellationToken)
    {
        var parameters = new
        {
            Id = request.Id
        };

        var locacaoById = await _repositoryDapper.GetById<Locacao>(request.Id,request.Query!, parameters);

        var verifyDateTermino = DateTime.Compare(request.DataDevolucao, locacaoById.DataTermino);

        if (verifyDateTermino < 0)
        {
            var daysLessEndContract =  locacaoById.DataTermino - request.DataDevolucao;

            switch (locacaoById.PrazoEmDias) {
                case 7:
                    decimal valueBreakForDay7Days = ((decimal)Multas._7dias / 100);
                    locacaoById.ValorMulta = daysLessEndContract.Days * (locacaoById.ValorDiaria * valueBreakForDay7Days);
                    break;
                case 15:
                    decimal valueBreakForDay15Days = ((decimal)Multas._15dias / 100);
                    locacaoById.ValorMulta = daysLessEndContract.Days * (locacaoById.ValorDiaria * valueBreakForDay15Days);
                    break;
                case 30:
                    decimal valueBreakForDay30Days = ((decimal)Multas._30dias / 100);
                    locacaoById.ValorMulta = daysLessEndContract.Days * (locacaoById.ValorDiaria * valueBreakForDay30Days);
                    break;
            }

            locacaoById.ValorTotal += (decimal)locacaoById.ValorMulta!;
        }
        else if (verifyDateTermino > 0) {
            var daysLessEndContractFuture = request.DataDevolucao - locacaoById.DataTermino;
            locacaoById.ValorAdicional = daysLessEndContractFuture.Days * (50 + locacaoById.ValorDiaria);
            locacaoById.ValorTotal += (decimal)locacaoById.ValorAdicional;
        }

        return locacaoById;
    }
}
