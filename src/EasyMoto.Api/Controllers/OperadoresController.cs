using EasyMoto.Api.Hypermedia;
using EasyMoto.Application.Operadores;
using EasyMoto.Application.Operadores.Contracts;
using EasyMoto.Application.Shared.Hateoas;
using EasyMoto.Application.Shared.Pagination;
using Microsoft.AspNetCore.Mvc;

namespace EasyMoto.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public sealed class OperadoresController : ControllerBase
    {
        private readonly ListarOperadoresHandler _list;
        private readonly ObterOperadorPorIdHandler _getById;
        private readonly CriarOperadorHandler _create;
        private readonly AtualizarOperadorHandler _update;
        private readonly ExcluirOperadorHandler _delete;

        public OperadoresController(
            ListarOperadoresHandler list,
            ObterOperadorPorIdHandler getById,
            CriarOperadorHandler create,
            AtualizarOperadorHandler update,
            ExcluirOperadorHandler delete)
        {
            _list = list;
            _getById = getById;
            _create = create;
            _update = update;
            _delete = delete;
        }

        [HttpGet(Name = "GetOperadores")]
        [ProducesResponseType(typeof(PagedResource<OperadorResponse>), StatusCodes.Status200OK)]
        public async Task<ActionResult<PagedResource<OperadorResponse>>> Get([FromQuery] int page = 1, [FromQuery] int size = 20, CancellationToken ct = default)
        {
            var result = await _list.ExecuteAsync(new PageQuery(page, size), ct);
            var itemsCount = result.Items.Count();
            var links = HateoasBuilder.PagingLinksByCount(Url, "GetOperadores", page, size, itemsCount);
            var payload = new PagedResource<OperadorResponse>(result.Items, page, size, itemsCount, links);
            return Ok(payload);
        }

        [HttpGet("{id:int}", Name = "GetOperadorById")]
        [ProducesResponseType(typeof(Resource<OperadorResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Resource<OperadorResponse>>> GetById(int id, CancellationToken ct = default)
        {
            var item = await _getById.ExecuteAsync(id, ct);
            if (item is null) return NotFound();
            var links = new[]
            {
                new Link("self", Url.Link("GetOperadorById", new { id })!, "GET"),
                new Link("update", Url.Link("UpdateOperador", new { id })!, "PUT"),
                new Link("delete", Url.Link("DeleteOperador", new { id })!, "DELETE"),
                new Link("collection", Url.Link("GetOperadores", new { page = 1, pageSize = 20 })!, "GET")
            };
            return Ok(new Resource<OperadorResponse>(item, links));
        }

        [HttpPost]
        [ProducesResponseType(typeof(Resource<OperadorResponse>), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Resource<OperadorResponse>>> Post([FromBody] CriarOperadorRequest req, CancellationToken ct = default)
        {
            var created = await _create.ExecuteAsync(req, ct);
            var links = new[]
            {
                new Link("self", Url.Link("GetOperadorById", new { id = created.Id })!, "GET"),
                new Link("update", Url.Link("UpdateOperador", new { id = created.Id })!, "PUT"),
                new Link("delete", Url.Link("DeleteOperador", new { id = created.Id })!, "DELETE"),
                new Link("collection", Url.Link("GetOperadores", new { page = 1, pageSize = 20 })!, "GET")
            };
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, new Resource<OperadorResponse>(created, links));
        }

        [HttpPut("{id:int}", Name = "UpdateOperador")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Put(int id, [FromBody] AtualizarOperadorRequest req, CancellationToken ct = default)
        {
            var cmd = new AtualizarOperadorRequest(
                id,
                req.NomeOperador,
                req.Cpf,
                req.Telefone,
                req.Email,
                req.FilialId
            );

            await _update.ExecuteAsync(cmd, ct);
            return NoContent();
        }
        [HttpDelete("{id:int}", Name = "DeleteOperador")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Delete(int id, CancellationToken ct = default)
        {
            await _delete.ExecuteAsync(id, ct);
            return NoContent();
        }

    }
}
