using BackEnd.Application.Dtos.Motos;
using BackEnd.Domain.Entities;
using BackEnd.Domain.SeedWork;
using MediatR;

namespace BackEnd.Application.CQRS.Motos.Write;

public class CreateMotoCommandHandler : IRequestHandler<CreateMotoCommand, Moto>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateMotoCommandHandler(IUnitOfWork unitOfWork)
        => _unitOfWork = unitOfWork;
    

    public async Task<Moto> Handle(CreateMotoCommand request, CancellationToken cancellationToken)
    {
        var newMoto = new Moto(request.Ano, request.Modelo, request.Placa, true);

        await _unitOfWork.Repository.AddObject<Moto>(newMoto);

        await _unitOfWork.CommitAsync();

        return newMoto;
    }
}
