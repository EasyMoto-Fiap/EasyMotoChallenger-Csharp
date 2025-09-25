using Microsoft.AspNetCore.Mvc;
using EasyMoto.Application.Localizacoes;
using EasyMoto.Application.Localizacoes.Contracts;
using EasyMoto.Application.Shared.Pagination;

namespace EasyMoto.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LocalizacoesController : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Get([FromServices] ListarLocalizacoesHandler handler, [FromQuery] int page = 1, [FromQuery] int size = 10, CancellationToken ct = default)
    {
        var result = await handler.ExecuteAsync(new PageQuery(page, size), ct);
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById([FromServices] ObterLocalizacaoPorIdHandler handler, Guid id, CancellationToken ct = default)
    {
        var item = await handler.ExecuteAsync(id, ct);
        if (item is null) return NotFound();
        return Ok(item);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromServices] CriarLocalizacaoHandler handler, [FromBody] CriarLocalizacaoRequest req, CancellationToken ct = default)
    {
        var created = await handler.ExecuteAsync(req, ct);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Put([FromServices] AtualizarLocalizacaoHandler handler, Guid id, [FromBody] AtualizarLocalizacaoRequest req, CancellationToken ct = default)
    {
        var ok = await handler.ExecuteAsync(id, req, ct);
        if (!ok) return NotFound();
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete([FromServices] ExcluirLocalizacaoHandler handler, Guid id, CancellationToken ct = default)
    {
        var ok = await handler.ExecuteAsync(id, ct);
        if (!ok) return NotFound();
        return NoContent();
    }
}