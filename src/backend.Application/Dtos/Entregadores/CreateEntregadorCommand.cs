using BackEnd.Domain.Entities;
using MediatR;

namespace BackEnd.Application.Dtos.Entregadores;

public class CreateEntregadorCommand : IRequest<Entregador>
{
    public string? Nome { get; set; }

    public string? CNH { get; set; }

    public string? CategoriaCNH { get; set; }

    public string? CNPJ { get; set; }

    public DateTime DataNascimento { get; set; }

    public string? NumeroCNH { get; set; }
}