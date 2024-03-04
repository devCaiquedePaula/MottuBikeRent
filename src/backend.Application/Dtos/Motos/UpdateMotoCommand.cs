using BackEnd.Domain.Entities;
using MediatR;

namespace BackEnd.Application.Dtos.Motos;

public class UpdateMotoCommand : CreateMotoCommand, IRequest<Moto>
{
    public Guid Id { get; set; }

}