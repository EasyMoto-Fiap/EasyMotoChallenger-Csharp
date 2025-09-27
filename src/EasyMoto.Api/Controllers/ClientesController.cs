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
        [HttpGet]
        public Task<PagedResult<ClienteResponse>> Get(
            [FromServices] ListarClientesHandler handler,
            [FromQuery] PageQuery query,
            CancellationToken ct) =>
            handler.ExecuteAsync(query, ct);

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ClienteResponse>> GetById(
            [FromServices] ObterClientePorIdHandler handler,
            [FromRoute] int id,
            CancellationToken ct)
        {
            var r = await handler.ExecuteAsync(id, ct);
            if (r is null) return NotFound();
            return r;
        }

        [HttpPost]
        public Task<ClienteResponse> Post(
            [FromServices] CriarClienteHandler handler,
            [FromBody] CriarClienteRequest req,
            CancellationToken ct) =>
            handler.ExecuteAsync(req, ct);

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put(
            [FromServices] AtualizarClienteHandler handler,
            [FromRoute] int id,
            [FromBody] AtualizarClienteRequest req,
            CancellationToken ct)
        {
            var ok = await handler.ExecuteAsync(id, req, ct);
            if (!ok) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(
            [FromServices] ExcluirClienteHandler handler,
            [FromRoute] int id,
            CancellationToken ct)
        {
            var ok = await handler.ExecuteAsync(id, ct);
            if (!ok) return NotFound();
            return NoContent();
        }
    }
}