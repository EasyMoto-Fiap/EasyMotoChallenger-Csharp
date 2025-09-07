using EasyMoto.Application.Motos;
using EasyMoto.Application.Motos.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace EasyMoto.Api.Controllers;

[ApiController]
[Route("api/motos")]
public sealed class MotosController : ControllerBase
{
    private readonly CriarMotoHandler _criar;
    private readonly ObterMotoPorIdHandler _obter;

    public MotosController(CriarMotoHandler criar, ObterMotoPorIdHandler obter)
    {
        _criar = criar;
        _obter = obter;
    }

    [HttpPost]
    public async Task<IActionResult> Criar([FromBody] CriarMotoRequest request, CancellationToken ct)
    {
        var result = await _criar.Handle(request, ct);
        return Created($"/api/motos/{result.IdMoto}", result);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> ObterPorId([FromRoute] int id, CancellationToken ct)
    {
        var result = await _obter.Handle(id, ct);
        if (result is null) return NotFound();
        return Ok(result);
    }
}
