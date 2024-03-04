using MediatR;
using BackEnd.Domain.Entities;

namespace BackEnd.Application.Dtos.Motos;

public class CreateMotoCommand : IRequest<Moto>
{
    public int Ano { get; set; }

    public string? Modelo { get; set; }

    public string? Placa { get; set; }
}
