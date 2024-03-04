using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using BackEnd.Application.Dtos.Entregadores;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BackEnd.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EntregadoresController : Controller
{
    private readonly IMediator _mediator;
    private readonly IConfiguration _configuration;

    public EntregadoresController(IMediator mediator, IConfiguration configuration)
    {
        _mediator = mediator;
        _configuration = configuration;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var query = new GetEntregadoresAll();
        var members = await _mediator.Send(query);

        return Ok(members);
    }

    [HttpGet("{numeroCNH}")]
    public async Task<IActionResult> GetAsync(string numeroCNH)
    {
        var query = new GetEntregadorById { NumeroCNH = numeroCNH.Trim() };
        var member = await _mediator.Send(query);

        return Ok(member);
    }

    [HttpGet("EntregadoresNotificados/{pedidoId}")]
    public async Task<IActionResult> GetAsync(Guid pedidoId)
    {
        var query = new GetEntregadorByNotificacao { PedidoId = pedidoId };
        var member = await _mediator.Send(query);

        return Ok(member);
    }

    [HttpPost]
    public async Task<IActionResult> PostAsync(CreateEntregadorCommand createEntregador)
    {
        var createdEntregador = await _mediator.Send(createEntregador);

        if (createdEntregador.CategoriaCNH == "I")
            return BadRequest("Categoria Inválida");

        return Created(nameof(EntregadoresController), createdEntregador);

    }

    [HttpPut]
    public async Task<IActionResult> PutAsync(UpdateEntregadorCommand updateEntregador)
    {
        var updatedEntregador = await _mediator.Send(updateEntregador);

        if (updatedEntregador.CategoriaCNH == "I")
            return BadRequest("Categoria Inválida");

        return updatedEntregador != null ? Ok(updatedEntregador) : NotFound("Member not found.");

    }

    [HttpPatch("AtualizarCNH/{id}")]
    public async Task<IActionResult> PatchAsync(Guid id, UpdateEntregadorCNHCommand command)
    {
        command.Id = id;
        var updatedEntregador = await _mediator.Send(command);

        return updatedEntregador != null ? Ok(updatedEntregador) : NotFound("Member not found.");

    }

    [HttpPatch("SalvarImagem")]
    public async Task<IActionResult> PatchAsync(IFormFile cnh)
    {
        string? nameFile;
        string? formatFile;

        using (Stream image = cnh.OpenReadStream())
        {
            nameFile = PrepareUrlAzureBlob(cnh.FileName, nameof(nameFile));
            formatFile = PrepareUrlAzureBlob(cnh.FileName, nameof(formatFile));

            if (string.IsNullOrWhiteSpace(formatFile))
                return BadRequest("Formato do Arquivo não permitido, favor enviar .png ou .bmp");

            BlobContainerClient containerClient = await GetCloudBlobContainer(_configuration["FullImageContainerName"]!);
            string blobName = $"{nameFile}-{Guid.NewGuid()}{formatFile}";
            BlobClient blobClient = containerClient.GetBlobClient(blobName.ToLower().Replace("-", String.Empty));
            var blobUploadOptions = new BlobUploadOptions
            {
                HttpHeaders = new BlobHttpHeaders
                {
                    ContentType = cnh.ContentType
                }
            };

            await blobClient.UploadAsync(image, blobUploadOptions);

            var urlArquivo = blobClient.Uri.AbsoluteUri;
            return Accepted(urlArquivo);
        }

    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        var command = new DeleteEntregadorCommand { Id = id };

        var deletedEntregador = await _mediator.Send(command);

        return deletedEntregador != null ? NoContent() : NotFound("Member not found.");
    }

    private async Task<BlobContainerClient> GetCloudBlobContainer(string containerName)
    {
        BlobServiceClient serviceClient = new BlobServiceClient(_configuration.GetConnectionString("StorageConnectionString"));
        BlobContainerClient containerClient = serviceClient.GetBlobContainerClient(containerName);
        await containerClient.CreateIfNotExistsAsync();
        return containerClient;
    }

    private string PrepareUrlAzureBlob(string filename, string method)
    {
        var listFileNames = filename.Split(".").ToList();
        string nameFile = listFileNames.FirstOrDefault()!;
        string formatFile = listFileNames.LastOrDefault()!;

        if(method == nameof(formatFile))
            if (!(formatFile.Trim().Equals("png") || formatFile.Trim().Equals("bmp")))
                return string.Empty;

        switch (method)
        {
            case nameof(nameFile):
                return nameFile;
            case nameof(formatFile):
                return $".{formatFile}";
            default:
                break;
        }

        return string.Empty;
    }
}