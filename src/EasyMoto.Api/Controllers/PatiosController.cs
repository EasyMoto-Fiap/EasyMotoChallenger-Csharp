using System;
using System.Threading;
using System.Threading.Tasks;
using EasyMoto.Application.Patios;
using EasyMoto.Application.Patios.Contracts;
using EasyMoto.Application.Shared.Pagination;
using Microsoft.AspNetCore.Mvc;

namespace EasyMoto.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PatiosController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Get([FromServices] ListarPatiosHandler handler, [FromQuery] int page = 1, [FromQuery] int size = 10, CancellationToken ct = default)
        {
            var result = await handler.ExecuteAsync(new PageQuery(page, size), ct);
            return Ok(result);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById([FromServices] ObterPatioPorIdHandler handler, Guid id, CancellationToken ct = default)
        {
            var r = await handler.ExecuteAsync(id, ct);
            if (r == null) return NotFound();
            return Ok(r);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromServices] CriarPatioHandler handler, [FromBody] CriarPatioRequest req, CancellationToken ct = default)
        {
            var r = await handler.ExecuteAsync(req, ct);
            return CreatedAtAction(nameof(GetById), new { id = r.Id }, r);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Put([FromServices] AtualizarPatioHandler handler, Guid id, [FromBody] AtualizarPatioRequest req, CancellationToken ct = default)
        {
            var ok = await handler.ExecuteAsync(id, req, ct);
            if (!ok) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete([FromServices] ExcluirPatioHandler handler, Guid id, CancellationToken ct = default)
        {
            await handler.ExecuteAsync(id, ct);
            return NoContent();
        }
    }
}