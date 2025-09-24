using EasyMoto.Application.Clientes;
using EasyMoto.Application.Clientes.Contracts;
using EasyMoto.Application.Shared.Pagination;
using Microsoft.AspNetCore.Mvc;

namespace EasyMoto.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public sealed class ClientesController : ControllerBase
    {
        private readonly ListarClientesHandler _list;
        private readonly ObterClientePorIdHandler _getById;
        private readonly CriarClienteHandler _create;
        private readonly AtualizarClienteHandler _update;
        private readonly ExcluirClienteHandler _delete;

        public ClientesController(
            ListarClientesHandler list,
            ObterClientePorIdHandler getById,
            CriarClienteHandler create,
            AtualizarClienteHandler update,
            ExcluirClienteHandler delete)
        {
            _list = list;
            _getById = getById;
            _create = create;
            _update = update;
            _delete = delete;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] int page = 1, [FromQuery] int size = 20, CancellationToken ct = default)
        {
            var result = await _list.ExecuteAsync(new PageQuery(page, size), ct);
            return Ok(result);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id, CancellationToken ct = default)
        {
            var result = await _getById.ExecuteAsync(id, ct);
            return result is null ? NotFound() : Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CriarClienteRequest req, CancellationToken ct = default)
        {
            var result = await _create.ExecuteAsync(req, ct);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] AtualizarClienteRequest req, CancellationToken ct = default)
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
