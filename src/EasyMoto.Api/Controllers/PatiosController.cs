using EasyMoto.Application.Patios;
using EasyMoto.Application.Patios.Contracts;
using EasyMoto.Application.Shared.Pagination;
using Microsoft.AspNetCore.Mvc;

namespace EasyMoto.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public sealed class PatiosController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CriarPatioRequest req, [FromServices] CriarPatioHandler handler, CancellationToken ct)
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);
            var patio = await handler.ExecuteAsync(req, ct);
            return CreatedAtAction(nameof(GetById), new { id = patio.IdPatio }, new
            {
                patio.IdPatio,
                patio.NomePatio,
                patio.TamanhoPatio,
                patio.Andar,
                patio.FilialId
            });
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id, [FromServices] ObterPatioPorIdHandler handler, CancellationToken ct)
        {
            var patio = await handler.ExecuteAsync(id, ct);
            if (patio is null) return NotFound();
            return Ok(patio);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put(int id, [FromBody] AtualizarPatioRequest req, [FromServices] AtualizarPatioHandler handler, CancellationToken ct)
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);
            var ok = await handler.ExecuteAsync(id, req, ct);
            return ok ? NoContent() : NotFound();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id, [FromServices] ExcluirPatioHandler handler, CancellationToken ct)
        {
            await handler.ExecuteAsync(id, ct);
            return NoContent();
        }

        [HttpGet]
        public Task<PagedResult<PatioResponse>> List([FromQuery] PageQuery query, [FromServices] ListarPatiosHandler handler, CancellationToken ct) =>
            handler.ExecuteAsync(query, ct);
    }
}
