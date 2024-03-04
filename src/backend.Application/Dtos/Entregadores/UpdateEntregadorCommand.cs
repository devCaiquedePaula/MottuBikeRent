using BackEnd.Domain.Entities;
using MediatR;

namespace BackEnd.Application.Dtos.Entregadores;

public class UpdateEntregadorCommand : CreateEntregadorCommand, IRequest<Entregador>
{
    public Guid Id { get; set; }

}