using BackEnd.Domain.Entities;
using MediatR;

namespace BackEnd.Application.Dtos.Motos;

public class UpdateMotoPlacaCommand : IRequest<Moto>
{
    public Guid Id { get; set; }

    public string? Placa { get; set; }
}