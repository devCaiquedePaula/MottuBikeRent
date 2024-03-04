using BackEnd.Domain.Entities;
using MediatR;

namespace BackEnd.Application.Dtos.Motos;

public class DeleteMotoCommand : IRequest<Moto>
{
    public Guid Id { get; set; }
}