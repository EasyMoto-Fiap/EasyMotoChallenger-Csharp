using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EasyMoto.Application.Motos;
using EasyMoto.Application.Motos.Contracts;
using EasyMoto.Application.Shared.Pagination;

namespace EasyMoto.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public sealed class MotosController : ControllerBase
    {
        private readonly ListarMotosHandler _list;
        private readonly ObterMotoPorIdHandler _getById;
        private readonly CriarMotoHandler _create;
        private readonly AtualizarMotoHandler _update;
        private readonly ExcluirMotoHandler _delete;

        public MotosController(
            ListarMotosHandler list,
            ObterMotoPorIdHandler getById,
            CriarMotoHandler create,
            AtualizarMotoHandler update,
            ExcluirMotoHandler delete)
        {
            _list = list;
            _getById = getById;
            _create = create;
            _update = update;
            _delete = delete;
        }

        [HttpGet]
        public async Task<ActionResult<PagedResult<MotoResponse>>> Get([FromQuery] int page = 1, [FromQuery] int size = 10, CancellationToken ct = default)
        {
            var result = await _list.ExecuteAsync(new PageQuery(page, size), ct);
            return Ok(result);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<MotoResponse?>> GetById(Guid id, CancellationToken ct = default)
        {
            var result = await _getById.ExecuteAsync(id, ct);
            if (result is null) return NotFound();
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<MotoResponse>> Post([FromBody] CriarMotoRequest req, CancellationToken ct = default)
        {
            var result = await _create.ExecuteAsync(req, ct);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] AtualizarMotoRequest req, CancellationToken ct = default)
        {
            await _update.ExecuteAsync(id, req, ct);
            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id, CancellationToken ct = default)
        {
            await _delete.ExecuteAsync(id, ct);
            return NoContent();
        }
    }
}
