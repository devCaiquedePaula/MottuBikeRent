using BackEnd.Domain.Entities;
using BackEnd.Domain.Enum;
using HunterDomain.Extensions;
using MediatR;

namespace BackEnd.Application.Dtos.Motos;

public class GetMotosAll : IRequest<IEnumerable<Moto>>
{
    private readonly Queries _query = new();

    public string? Query { get => _query.GetQuery((int)QueryCQRS.QueryMotosAll); }
}
