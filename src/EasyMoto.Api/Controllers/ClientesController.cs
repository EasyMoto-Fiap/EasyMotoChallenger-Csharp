using EasyMoto.Api.Hypermedia;
using EasyMoto.Api.Swagger.Examples.Clientes;
using EasyMoto.Application.Clientes;
using EasyMoto.Application.Clientes.Contracts;
using EasyMoto.Application.Shared.Hateoas;
using EasyMoto.Application.Shared.Pagination;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Filters;

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

        [HttpGet(Name = "GetClientes")]
        [ProducesResponseType(typeof(PagedResource<ClienteResponse>), StatusCodes.Status200OK)]
        public async Task<ActionResult<PagedResource<ClienteResponse>>> Get([FromQuery] int page = 1, [FromQuery] int size = 20, CancellationToken ct = default)
        {
            var result = await _list.ExecuteAsync(new PageQuery(page, size), ct);
            var itemsCount = result.Items.Count();
            var links = HateoasBuilder.PagingLinksByCount(Url, "GetClientes", page, size, itemsCount);
            var payload = new PagedResource<ClienteResponse>(result.Items, page, size, itemsCount, links);
            return Ok(payload);
        }

        [HttpGet("{id:guid}", Name = "GetClienteById")]
        [ProducesResponseType(typeof(Resource<ClienteResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(ResourceClienteResponseExample))]
        public async Task<ActionResult<Resource<ClienteResponse>>> GetById(Guid id, CancellationToken ct = default)
        {
            var item = await _getById.ExecuteAsync(id, ct);
            if (item is null) return NotFound();
            var links = new[]
            {
                new Link("self", Url.Link("GetClienteById", new { id })!, "GET"),
                new Link("update", Url.Link("UpdateCliente", new { id })!, "PUT"),
                new Link("delete", Url.Link("DeleteCliente", new { id })!, "DELETE"),
                new Link("collection", Url.Link("GetClientes", new { page = 1, pageSize = 20 })!, "GET")
            };
            return Ok(new Resource<ClienteResponse>(item, links));
        }

        [HttpPost]
        [ProducesResponseType(typeof(Resource<ClienteResponse>), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [SwaggerRequestExample(typeof(CriarClienteRequest), typeof(CriarClienteRequestExample))]
        [SwaggerResponseExample(StatusCodes.Status201Created, typeof(ResourceClienteResponseExample))]
        public async Task<ActionResult<Resource<ClienteResponse>>> Post([FromBody] CriarClienteRequest req, CancellationToken ct = default)
        {
            var created = await _create.ExecuteAsync(req, ct);
            var links = new[]
            {
                new Link("self", Url.Link("GetClienteById", new { id = created.Id })!, "GET"),
                new Link("update", Url.Link("UpdateCliente", new { id = created.Id })!, "PUT"),
                new Link("delete", Url.Link("DeleteCliente", new { id = created.Id })!, "DELETE"),
                new Link("collection", Url.Link("GetClientes", new { page = 1, pageSize = 20 })!, "GET")
            };
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, new Resource<ClienteResponse>(created, links));
        }

        [HttpPut("{id:guid}", Name = "UpdateCliente")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Put(Guid id, [FromBody] AtualizarClienteRequest req, CancellationToken ct = default)
        {
            await _update.ExecuteAsync(id, req, ct);
            return NoContent();
        }

        [HttpDelete("{id:guid}", Name = "DeleteCliente")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(Guid id, CancellationToken ct = default)
        {
            var ok = await _delete.ExecuteAsync(id, ct);
            if (!ok) return NotFound();
            return NoContent();
        }
    }
}
