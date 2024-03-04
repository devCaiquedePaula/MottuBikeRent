using BackEnd.Application.Dtos.Entregadores;
using BackEnd.Domain.Entities;
using BackEnd.Domain.SeedWork;
using MediatR;

namespace BackEnd.Application.CQRS.Entregadors.Write;

public class CreateEntregadorCommandHandler : IRequestHandler<CreateEntregadorCommand, Entregador>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateEntregadorCommandHandler(IUnitOfWork unitOfWork)
        => _unitOfWork = unitOfWork;
    

    public async Task<Entregador> Handle(CreateEntregadorCommand request, CancellationToken cancellationToken)
    {
        var categoryCNH = request.CategoriaCNH!.Trim();

        var isValidCategoryCNH = (categoryCNH.Equals("A") || categoryCNH.Equals("B") || categoryCNH.Equals("AB"));
        
        var newEntregador = new Entregador(request.Nome, request.CNH, request.CategoriaCNH, request.CNPJ, request.DataNascimento,
            request.NumeroCNH!, true);

        if (!isValidCategoryCNH)
        {
            newEntregador.CategoriaCNH = "I";
            return newEntregador;
        }

        await _unitOfWork.Repository.AddObject<Entregador>(newEntregador);

        await _unitOfWork.CommitAsync();

        return newEntregador;
    }
}
