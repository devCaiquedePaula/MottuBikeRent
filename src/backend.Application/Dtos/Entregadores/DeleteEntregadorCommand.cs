using BackEnd.Domain.Entities;
using MediatR;

namespace BackEnd.Application.Dtos.Entregadores;

public class DeleteEntregadorCommand : IRequest<Entregador>
{
    public Guid Id { get; set; }
}