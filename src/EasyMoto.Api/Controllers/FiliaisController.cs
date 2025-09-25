using EasyMoto.Application.Filiais;
using EasyMoto.Application.Filiais.Contracts;
using EasyMoto.Application.Shared.Pagination;
using Microsoft.AspNetCore.Mvc;

namespace EasyMoto.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public sealed class FiliaisController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Get([FromServices] ListarFiliaisHandler handler, [FromQuery] int page = 1, [FromQuery] int size = 10, CancellationToken ct = default)
        {
            var result = await handler.ExecuteAsync(new PageQuery(page, size), ct);
            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromServices] ObterFilialPorIdHandler handler, int id, CancellationToken ct = default)
        {
            var item = await handler.ExecuteAsync(id, ct);
            if (item == null) return NotFound();
            return Ok(item);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromServices] CriarFilialHandler handler, [FromBody] CriarFilialRequest req, CancellationToken ct = default)
        {
            var created = await handler.ExecuteAsync(req, ct);
            return CreatedAtAction(nameof(GetById), new { id = created.IdFilial }, created);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put([FromServices] AtualizarFilialHandler handler, int id, [FromBody] AtualizarFilialRequest req, CancellationToken ct = default)
        {
            var updated = await handler.ExecuteAsync(id, req, ct);
            if (updated == null) return NotFound();
            return Ok(updated);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromServices] ExcluirFilialHandler handler, int id, CancellationToken ct = default)
        {
            var ok = await handler.ExecuteAsync(id, ct);
            if (!ok) return NotFound();
            return NoContent();
        }
    }
}
