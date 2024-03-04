using BackEnd.Domain.Entities;
using BackEnd.Domain.Enum;
using HunterDomain.Extensions;
using MediatR;

namespace BackEnd.Application.Dtos.Motos;

public class GetMotoById : IRequest<Moto>
{
    private readonly Queries _query = new();

    public Guid Id { get; set; }

    public string? Placa { get; set; }

    public string? Query { get => _query.GetQuery((int)QueryCQRS.QuetyMotosPorPlaca); }
}
