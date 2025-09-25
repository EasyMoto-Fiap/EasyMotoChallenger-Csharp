using EasyMoto.Application.Funcionarios;
using EasyMoto.Application.Funcionarios.Contracts;
using EasyMoto.Application.Shared.Pagination;
using Microsoft.AspNetCore.Mvc;

namespace EasyMoto.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public sealed class FuncionariosController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Get([FromServices] ListarFuncionariosHandler handler, [FromQuery] int page = 1, [FromQuery] int size = 10, CancellationToken ct = default)
        {
            var result = await handler.ExecuteAsync(new PageQuery(page, size), ct);
            return Ok(result);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById([FromServices] ObterFuncionarioPorIdHandler handler, Guid id, CancellationToken ct = default)
        {
            var item = await handler.ExecuteAsync(id, ct);
            if (item is null) return NotFound();
            return Ok(item);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromServices] CriarFuncionarioHandler handler, [FromBody] CriarFuncionarioRequest req, CancellationToken ct = default)
        {
            var created = await handler.ExecuteAsync(req, ct);
            return CreatedAtAction(nameof(GetById), new { id = created.IdFuncionario }, created);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Put([FromServices] AtualizarFuncionarioHandler handler, Guid id, [FromBody] AtualizarFuncionarioRequest req, CancellationToken ct = default)
        {
            var updated = await handler.ExecuteAsync(id, req, ct);
            if (updated is null) return NotFound();
            return Ok(updated);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete([FromServices] ExcluirFuncionarioHandler handler, Guid id, CancellationToken ct = default)
        {
            var ok = await handler.ExecuteAsync(id, ct);
            if (!ok) return NotFound();
            return NoContent();
        }
    }
}
