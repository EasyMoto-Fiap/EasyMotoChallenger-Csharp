using EasyMoto.Application.Operadores;
using EasyMoto.Application.Operadores.Contracts;
using EasyMoto.Application.Shared.Pagination;
using EasyMoto.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace EasyMoto.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OperadoresController : ControllerBase
    {
        private readonly CriarOperadorHandler _create;
        private readonly AtualizarOperadorHandler _update;
        private readonly ExcluirOperadorHandler _delete;
        private readonly ObterOperadorPorIdHandler _getById;
        private readonly ListarOperadoresHandler _list;

        public OperadoresController(IOperadorRepository repo)
        {
            _create = new CriarOperadorHandler(repo);
            _update = new AtualizarOperadorHandler(repo);
            _delete = new ExcluirOperadorHandler(repo);
            _getById = new ObterOperadorPorIdHandler(repo);
            _list = new ListarOperadoresHandler(repo);
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] int page = 1, [FromQuery] int size = 10, CancellationToken ct = default)
        {
            var result = await _list.ExecuteAsync(new PageQuery(page, size), ct);
            return Ok(result);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id, CancellationToken ct)
        {
            var result = await _getById.ExecuteAsync(id, ct);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CriarOperadorRequest req, CancellationToken ct)
        {
            var result = await _create.ExecuteAsync(req, ct);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] AtualizarOperadorRequest req, CancellationToken ct)
        {
            if (id != req.Id) return BadRequest();
            var ok = await _update.ExecuteAsync(req, ct);
            if (!ok) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
        {
            await _delete.ExecuteAsync(id, ct);
            return NoContent();
        }
    }
}
