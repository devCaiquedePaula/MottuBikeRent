using BackEnd.Domain.Entities;
using MediatR;

namespace BackEnd.Application.Dtos.Entregadores;

public class UpdateEntregadorCNHCommand : IRequest<Entregador>
{
    public Guid Id { get; set; }

    public string? CNH { get; set; }
}