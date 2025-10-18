using Asp.Versioning;
using EasyMoto.Application.Motos;
using EasyMoto.Application.Motos.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace EasyMoto.Api.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/motos")]
public sealed class MotosController : ControllerBase
{
    private readonly CriarMotoHandler _criar;
    private readonly ObterMotoPorIdHandler _obter;
    private readonly ListarMotosHandler _listar;
    private readonly AtualizarMotoHandler _atualizar;
    private readonly ExcluirMotoHandler _excluir;


    public MotosController(
        CriarMotoHandler criar,
        ObterMotoPorIdHandler obter,
        ListarMotosHandler listar,
        AtualizarMotoHandler atualizar,
        ExcluirMotoHandler excluir)
    {
        _criar = criar;
        _obter = obter;
        _listar = listar;
        _atualizar = atualizar;
        _excluir = excluir;
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

    [HttpGet]
    public async Task<IActionResult> Listar(CancellationToken ct)
    {
        var result = await _listar.Handle(ct);
        return Ok(result);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Atualizar([FromRoute] int id, [FromBody] AtualizarMotoRequest request, CancellationToken ct)
    {
        var result = await _atualizar.Handle(id, request, ct);
        if (result is null) return NotFound();
        return Ok(result);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Excluir([FromRoute] int id, CancellationToken ct)
    {
        var ok = await _excluir.Handle(id, ct);
        if (!ok) return NotFound();
        return NoContent();
    }

}
