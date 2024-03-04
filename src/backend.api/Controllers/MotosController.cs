using BackEnd.Application.Dtos.Motos;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BackEnd.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MotosController : Controller
{
    private readonly IMediator _mediator;

    public MotosController(IMediator mediator)
        => _mediator = mediator;
    
    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var query = new GetMotosAll();
        var members = await _mediator.Send(query);

        return Ok(members);
    }

    [HttpGet("{placa}")]
    public async Task<IActionResult> GetAsync(string placa)
    {
        var query = new GetMotoById{ Placa = placa.Trim() };
        var member = await _mediator.Send(query);

        return Ok(member);
    }

    [HttpPost]
    public async Task<IActionResult> PostAsync([FromBody] CreateMotoCommand createMoto)
    {
        var createdMoto = await _mediator.Send(createMoto);

        return Created(nameof(MotosController), createdMoto);
        
    }

    [HttpPut]
    public async Task<IActionResult> PutAsync(UpdateMotoCommand command)
    {
        var updatedMember = await _mediator.Send(command);

        return updatedMember != null ? Ok(updatedMember) : NotFound("Member not found.");

    }

    [HttpPatch("AlteraPlaca/{id}")]
    public async Task<IActionResult> PatchAsync(Guid id, UpdateMotoPlacaCommand command)
    {
        command.Id = id;
        var updatedMember = await _mediator.Send(command);

        return updatedMember != null ? Ok(updatedMember) : NotFound("Member not found.");

    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        var command = new DeleteMotoCommand { Id = id };

        var deletedMoto = await _mediator.Send(command);

        if (deletedMoto == default)
            return BadRequest("Moto possui locações não pode ser excluída");

        return deletedMoto != null ? NoContent() : NotFound("Member not found.");
    }
}