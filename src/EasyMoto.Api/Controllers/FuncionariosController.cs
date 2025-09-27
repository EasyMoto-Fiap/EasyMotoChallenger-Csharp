using EasyMoto.Application.Funcionarios;
using EasyMoto.Application.Funcionarios.Contracts;
using EasyMoto.Application.Shared.Pagination;
using Microsoft.AspNetCore.Mvc;

namespace EasyMoto.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class FuncionariosController : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Post([FromServices] CriarFuncionarioHandler handler, [FromBody] CriarFuncionarioRequest req, CancellationToken ct)
    {
        var result = await handler.ExecuteAsync(req, ct);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Put(int id, [FromServices] AtualizarFuncionarioHandler handler, [FromBody] AtualizarFuncionarioRequest req, CancellationToken ct)
    {
        req.Id = id;
        var ok = await handler.ExecuteAsync(req, ct);
        if (!ok) return NotFound();
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id, [FromServices] ExcluirFuncionarioHandler handler, CancellationToken ct)
    {
        var ok = await handler.ExecuteAsync(id, ct);
        if (!ok) return NotFound();
        return NoContent();
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id, [FromServices] ObterFuncionarioPorIdHandler handler, CancellationToken ct)
    {
        var result = await handler.ExecuteAsync(id, ct);
        if (result is null) return NotFound();
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromServices] ListarFuncionariosHandler handler, [FromQuery] int page = 1, [FromQuery] int size = 10, CancellationToken ct = default)
    {
        var result = await handler.ExecuteAsync(new PageQuery(page, size), ct);
        return Ok(result);
    }
}