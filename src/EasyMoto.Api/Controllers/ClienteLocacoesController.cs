using EasyMoto.Application.ClienteLocacoes;
using EasyMoto.Application.ClienteLocacoes.Contracts;
using EasyMoto.Application.Shared.Pagination;
using Microsoft.AspNetCore.Mvc;

namespace EasyMoto.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class ClienteLocacoesController : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<ClienteLocacaoResponse>> Post(
        [FromServices] CriarClienteLocacaoHandler handler,
        [FromBody] CriarClienteLocacaoRequest request,
        CancellationToken ct)
    {
        var result = await handler.ExecuteAsync(request, ct);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<ClienteLocacaoResponse>> GetById(
        [FromServices] ObterClienteLocacaoPorIdHandler handler,
        [FromRoute] int id,
        CancellationToken ct)
    {
        var result = await handler.ExecuteAsync(id, ct);
        if (result is null) return NotFound();
        return Ok(result);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> Put(
        [FromServices] AtualizarClienteLocacaoHandler handler,
        [FromRoute] int id,
        [FromBody] AtualizarClienteLocacaoRequest request,
        CancellationToken ct)
    {
        var ok = await handler.ExecuteAsync(id, request, ct);
        if (!ok) return NotFound();
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Delete(
        [FromServices] ExcluirClienteLocacaoHandler handler,
        [FromRoute] int id,
        CancellationToken ct)
    {
        var ok = await handler.ExecuteAsync(id, ct);
        if (!ok) return NotFound();
        return NoContent();
    }

    [HttpGet]
    public async Task<ActionResult<PagedResult<ClienteLocacaoResponse>>> Get(
        [FromServices] ListarClienteLocacoesHandler handler,
        [FromQuery] PageQuery query,
        CancellationToken ct)
    {
        var result = await handler.ExecuteAsync(query, ct);
        return Ok(result);
    }
}
