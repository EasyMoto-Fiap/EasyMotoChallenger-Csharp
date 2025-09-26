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
    public class OperadoresController : ControllerBase
    {
        private readonly CriarOperadorHandler _criar;
        private readonly AtualizarOperadorHandler _atualizar;
        private readonly ObterOperadorPorIdHandler _obter;
        private readonly ListarOperadoresHandler _listar;
        private readonly ExcluirOperadorHandler _excluir;

        public OperadoresController(
            CriarOperadorHandler criar,
            AtualizarOperadorHandler atualizar,
            ObterOperadorPorIdHandler obter,
            ListarOperadoresHandler listar,
            ExcluirOperadorHandler excluir)
        {
            _criar = criar;
            _atualizar = atualizar;
            _obter = obter;
            _listar = listar;
            _excluir = excluir;
        }

        [HttpGet(Name = "GetOperadores")]
        public async Task<ActionResult<PagedResource<OperadorResponse>>> Get([FromQuery] PageQuery query, CancellationToken ct)
        {
            var result = await _listar.ExecuteAsync(query, ct);
            var itemsCount = result.Items.Count();
            var links = HateoasBuilder.PagingLinksByCount(Url, "GetOperadores", query.Page, query.Size, itemsCount);
            var payload = new PagedResource<OperadorResponse>(result.Items, query.Page, query.Size, itemsCount, links);
            return Ok(payload);
        }

        [HttpGet("{id:int}", Name = "GetOperadorById")]
        public async Task<ActionResult<Resource<OperadorResponse>>> GetById(int id, CancellationToken ct)
        {
            var item = await _obter.ExecuteAsync(id, ct);
            if (item is null) return NotFound();
            var links = new[]
            {
                new Link("self", Url.Link("GetOperadorById", new { id })!, "GET"),
                new Link("update", Url.Link("UpdateOperador", new { id })!, "PUT"),
                new Link("delete", Url.Link("DeleteOperador", new { id })!, "DELETE"),
                new Link("collection", Url.Link("GetOperadores", new { page = 1, pageSize = 10 })!, "GET")
            };
            return Ok(new Resource<OperadorResponse>(item, links));
        }

        [HttpPost]
        public async Task<ActionResult<Resource<OperadorResponse>>> Post([FromBody] CriarOperadorRequest request, CancellationToken ct)
        {
            var created = await _criar.ExecuteAsync(request, ct);
            var links = new[]
            {
                new Link("self", Url.Link("GetOperadorById", new { id = created.Id })!, "GET"),
                new Link("update", Url.Link("UpdateOperador", new { id = created.Id })!, "PUT"),
                new Link("delete", Url.Link("DeleteOperador", new { id = created.Id })!, "DELETE"),
                new Link("collection", Url.Link("GetOperadores", new { page = 1, pageSize = 10 })!, "GET")
            };
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, new Resource<OperadorResponse>(created, links));
        }

        [HttpPut("{id:int}", Name = "UpdateOperador")]
        public async Task<IActionResult> Put(int id, [FromBody] AtualizarOperadorRequest request, CancellationToken ct)
        {
            if (id != request.Id) return BadRequest();
            var ok = await _atualizar.ExecuteAsync(request, ct);
            if (!ok) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id:int}", Name = "DeleteOperador")]
        public async Task<IActionResult> Delete(int id, CancellationToken ct)
        {
            await _excluir.ExecuteAsync(id, ct);
            return NoContent();
        }
    }
}
