using EasyMoto.Application.Patios;
using EasyMoto.Application.Patios.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace EasyMoto.Api.Controllers;

[ApiController]
[Route("api/patios")]
public sealed class PatiosController : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<PatioResponse>> Post([FromServices] CriarPatioHandler handler, [FromBody] CriarPatioRequest req, CancellationToken ct)
    {
        var resp = await handler.ExecuteAsync(req, ct);
        return CreatedAtAction(nameof(GetById), new { id = resp.Id }, resp);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<PatioResponse>> GetById([FromServices] ObterPatioPorIdHandler handler, int id, CancellationToken ct)
    {
        var resp = await handler.ExecuteAsync(id, ct);
        if (resp is null) return NotFound();
        return Ok(resp);
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<PatioResponse>>> Get([FromServices] ListarPatiosHandler handler, CancellationToken ct)
    {
        var list = await handler.ExecuteAsync(ct);
        return Ok(list);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Put([FromServices] AtualizarPatioHandler handler, int id, [FromBody] AtualizarPatioRequest req, CancellationToken ct)
    {
        var ok = await handler.ExecuteAsync(id, req, ct);
        if (!ok) return NotFound();
        return NoContent();
    }
}