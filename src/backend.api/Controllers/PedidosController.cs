using BackEnd.Application.Dtos.Pedidos;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BackEnd.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PedidosController : Controller
{
    private readonly IMediator _mediator;

    public PedidosController(IMediator mediator)
        => _mediator = mediator;
    
    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var query = new GetPedidosAll();
        var members = await _mediator.Send(query);

        return Ok(members);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAsync(Guid id)
    {
        var query = new GetPedidoById{ Id = id };
        var member = await _mediator.Send(query);

        return Ok(member);
    }

    [HttpPost("CriarPedido")]
    public async Task<IActionResult> PostAsync([FromBody] CreatePedidoCommand createPedido)
    {
        var createdPedido = await _mediator.Send(createPedido);

        return Created(nameof(PedidosController), createdPedido);
        
    }

    [HttpPut("AtualizaPedido")]
    public async Task<IActionResult> PutAsync(UpdatePedidoCommand command)
    {
        var updatedMember = await _mediator.Send(command);

        return updatedMember != null ? Ok(updatedMember) : NotFound("Member not found.");

    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        var command = new DeletePedidoCommand { Id = id };

        var deletedPedido = await _mediator.Send(command);

        return deletedPedido != null ? NoContent() : NotFound("Member not found.");
    }
}