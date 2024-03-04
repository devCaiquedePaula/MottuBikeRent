using BackEnd.Application.Dtos.Locacoes;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BackEnd.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LocacoesController : Controller
{
    private readonly IMediator _mediator;

    public LocacoesController(IMediator mediator)
        => _mediator = mediator;
    
    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var query = new GetLocacoesAll();
        var members = await _mediator.Send(query);

        return Ok(members);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAsync(Guid id)
    {
        var query = new GetLocacaoById{ Id = id };
        var member = await _mediator.Send(query);

        return Ok(member);
    }

    [HttpGet("{id}/{dataDevolucao}")]
    public async Task<IActionResult> GetValueFinalAsync(Guid id,DateTime dataDevolucao)
    {
        var query = new GetLocacaoValorTotalById { Id = id , DataDevolucao = dataDevolucao };
        var member = await _mediator.Send(query);

        return Ok(member);
    }

    [HttpPost]
    public async Task<IActionResult> PostAsync([FromBody] CreateLocacaoCommand createLocacao)
    {
        var createdLocacao = await _mediator.Send(createLocacao);

        if (createdLocacao == default)
            return BadRequest("Plano não disponível para locação, envie 7, 15 ou 30 dias, uma Data igual ou maior que hoje e uma categoria A ou AB.");

        return Created(nameof(LocacoesController), createdLocacao);
        
    }

    [HttpPut]
    public async Task<IActionResult> PutAsync(UpdateLocacaoCommand command)
    {
        var updatedLocacao = await _mediator.Send(command);

        if (updatedLocacao == default)
            return BadRequest("Plano não disponível para locação, envie 7, 15 ou 30 dias, uma Data igual ou maior que hoje e uma categoria A ou AB..");

        return updatedLocacao != null ? Ok(updatedLocacao) : NotFound("Member not found.");

    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        var command = new DeleteLocacaoCommand { Id = id };

        var deletedLocacao = await _mediator.Send(command);

        return deletedLocacao != null ? NoContent() : NotFound("Member not found.");
    }
}