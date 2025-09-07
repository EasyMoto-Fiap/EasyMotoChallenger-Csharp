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

    public ClientesController(CriarClienteHandler criar, ObterClientePorIdHandler obter)
    {
        _criar = criar;
        _obter = obter;
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
}
