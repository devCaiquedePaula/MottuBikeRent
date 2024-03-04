using BackEnd.Domain.Entities;
using BackEnd.Domain.Enum;
using HunterDomain.Extensions;
using MediatR;

namespace BackEnd.Application.Dtos.Entregadores;

public class GetEntregadorById : IRequest<Entregador>
{
    private readonly Queries _query = new();

    public Guid Id { get; set; }

    public string? NumeroCNH { get; set; }

    public string? Query { get => _query.GetQuery((int)QueryCQRS.QueryEntregadorByNumberCNH); }
}
