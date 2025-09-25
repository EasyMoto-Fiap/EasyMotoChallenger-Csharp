using System.Threading;
using System.Threading.Tasks;
using EasyMoto.Application.Empresas;
using EasyMoto.Application.Empresas.Contracts;
using EasyMoto.Application.Shared.Pagination;
using Microsoft.AspNetCore.Mvc;

namespace EasyMoto.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmpresasController : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Get([FromServices] ListarEmpresasHandler handler, [FromQuery] int page = 1, [FromQuery] int size = 10, CancellationToken ct = default)
    {
        var result = await handler.ExecuteAsync(new PageQuery(page, size), ct);
        return Ok(result);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById([FromServices] ObterEmpresaPorIdHandler handler, int id, CancellationToken ct = default)
    {
        var e = await handler.ExecuteAsync(id, ct);
        if (e is null) return NotFound();
        return Ok(e);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromServices] CriarEmpresaHandler handler, [FromBody] CriarEmpresaRequest req, CancellationToken ct = default)
    {
        var created = await handler.ExecuteAsync(req, ct);
        return CreatedAtAction(nameof(GetById), new { id = created.IdEmpresa }, created);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Put([FromServices] AtualizarEmpresaHandler handler, int id, [FromBody] AtualizarEmpresaRequest req, CancellationToken ct = default)
    {
        var updated = await handler.ExecuteAsync(id, req, ct);
        if (updated is null) return NotFound();
        return Ok(updated);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete([FromServices] ExcluirEmpresaHandler handler, int id, CancellationToken ct = default)
    {
        var ok = await handler.ExecuteAsync(id, ct);
        if (!ok) return NotFound();
        return NoContent();
    }
}