using EasyMoto.Application.Shared.Pagination;
using EasyMoto.Application.Localizacoes;
using EasyMoto.Application.Localizacoes.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace EasyMoto.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class LocalizacoesController : ControllerBase
{
    [HttpGet]
    public async Task<PagedResult<LocalizacaoResponse>> Get(
        ListarLocalizacoesHandler handler,
        [FromQuery] PageQuery query,
        CancellationToken ct)
        => await handler.ExecuteAsync(query, ct);

    [HttpGet("{id:int}")]
    public async Task<ActionResult<LocalizacaoResponse>> GetById(
        ObterLocalizacaoPorIdHandler handler,
        [FromRoute] int id,
        CancellationToken ct)
    {
        var result = await handler.ExecuteAsync(id, ct);
        if (result is null) return NotFound();
        return result;
    }

    [HttpPost]
    public async Task<ActionResult<LocalizacaoResponse>> Post(
        CriarLocalizacaoHandler handler,
        [FromBody] CriarLocalizacaoRequest req,
        CancellationToken ct)
    {
        var created = await handler.ExecuteAsync(req, ct);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Put(
        AtualizarLocalizacaoHandler handler,
        [FromRoute] int id,
        [FromBody] AtualizarLocalizacaoRequest req,
        CancellationToken ct)
    {
        req.Id = id;
        var ok = await handler.ExecuteAsync(req, ct);
        if (!ok) return NotFound();
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(
        ExcluirLocalizacaoHandler handler,
        [FromRoute] int id,
        CancellationToken ct)
    {
        var ok = await handler.ExecuteAsync(id, ct);
        if (!ok) return NotFound();
        return NoContent();
    }
}