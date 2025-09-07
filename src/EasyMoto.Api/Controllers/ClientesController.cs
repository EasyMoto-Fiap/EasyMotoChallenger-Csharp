using EasyMoto.Application.Clientes;
using EasyMoto.Application.Clientes.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace EasyMoto.Api.Controllers;

[ApiController]
[Route("api/clientes")]
public sealed class ClientesController : ControllerBase
{
    private readonly CriarClienteHandler _criar;
    private readonly ObterClientePorIdHandler _obter;
    private readonly ListarClientesHandler _listar;
    private readonly AtualizarClienteHandler _atualizar;

    public ClientesController(
        CriarClienteHandler criar,
        ObterClientePorIdHandler obter,
        ListarClientesHandler listar,
        AtualizarClienteHandler atualizar)
    {
        _criar = criar;
        _obter = obter;
        _listar = listar;
        _atualizar = atualizar;
    }

    [HttpPost]
    public async Task<IActionResult> Criar([FromBody] CriarClienteRequest request, CancellationToken ct)
    {
        var result = await _criar.Handle(request, ct);
        return Created($"/api/clientes/{result.IdCliente}", result);
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
    public async Task<IActionResult> Atualizar([FromRoute] int id, [FromBody] AtualizarClienteRequest request, CancellationToken ct)
    {
        var result = await _atualizar.Handle(id, request, ct);
        if (result is null) return NotFound();
        return Ok(result);
    }
}
