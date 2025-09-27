using EasyMoto.Application.Shared.Pagination;
using EasyMoto.Application.Vagas;
using EasyMoto.Application.Vagas.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace EasyMoto.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class VagasController : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Get([FromServices] ListarVagasHandler handler, [FromQuery] int page = 1, [FromQuery] int size = 10, CancellationToken ct = default)
    {
        var result = await handler.ExecuteAsync(new PageQuery(page, size), ct);
        return Ok(result);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById([FromServices] ObterVagaPorIdHandler handler, int id, CancellationToken ct = default)
    {
        var r = await handler.ExecuteAsync(id, ct);
        if (r == null) return NotFound();
        return Ok(r);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromServices] CriarVagaHandler handler, [FromBody] CriarVagaRequest req, CancellationToken ct = default)
    {
        var r = await handler.ExecuteAsync(req, ct);
        return CreatedAtAction(nameof(GetById), new { id = r.Id }, r);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Put([FromServices] AtualizarVagaHandler handler, int id, [FromBody] AtualizarVagaRequest req, CancellationToken ct = default)
    {
        var ok = await handler.ExecuteAsync(id, req, ct);
        if (!ok) return NotFound();
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete([FromServices] ExcluirVagaHandler handler, int id, CancellationToken ct = default)
    {
        await handler.ExecuteAsync(id, ct);
        return NoContent();
    }
}