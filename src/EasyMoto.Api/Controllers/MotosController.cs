using EasyMoto.Application.Motos;
using EasyMoto.Application.Motos.Contracts;
using EasyMoto.Application.Shared.Pagination;
using Microsoft.AspNetCore.Mvc;

namespace EasyMoto.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class MotosController : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Get([FromServices] ListarMotosHandler handler, [FromQuery] int page = 1, [FromQuery] int size = 10, CancellationToken ct = default)
    {
        var result = await handler.ExecuteAsync(new PageQuery(page, size), ct);
        return Ok(result);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById([FromServices] ObterMotoPorIdHandler handler, int id, CancellationToken ct = default)
    {
        var r = await handler.ExecuteAsync(id, ct);
        if (r is null) return NotFound();
        return Ok(r);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromServices] CriarMotoHandler handler, [FromBody] CriarMotoRequest req, CancellationToken ct = default)
    {
        var r = await handler.ExecuteAsync(req, ct);
        return CreatedAtAction(nameof(GetById), new { id = r.Id }, r);
    }

    [HttpPut]
    public async Task<IActionResult> Put([FromServices] AtualizarMotoHandler handler, [FromBody] AtualizarMotoRequest req, CancellationToken ct = default)
    {
        var ok = await handler.ExecuteAsync(req, ct);
        if (!ok) return NotFound();
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete([FromServices] ExcluirMotoHandler handler, int id, CancellationToken ct = default)
    {
        await handler.ExecuteAsync(id, ct);
        return NoContent();
    }
}