using BackEnd.Domain.Entities;
using MediatR;

namespace BackEnd.Application.Dtos.Locacoes;

public class UpdateLocacaoCommand : CreateLocacaoCommand, IRequest<Locacao>
{
    public Guid Id { get; set; }

    public string? Plano { get; set; }

    public DateTime DataCriacao { get; set; }

    public DateTime DataInicio { get; set; }

    public DateTime DataTermino { get; set; }

    public DateTime DataPrevistaTermino { get; set; }

    public decimal ValorDiaria { get; set; }

    public decimal? ValorAdicional { get; set; }

    public decimal? ValorMulta { get; set; }

    public decimal ValorTotal { get; set; }

    public string? Status { get; set; }

}