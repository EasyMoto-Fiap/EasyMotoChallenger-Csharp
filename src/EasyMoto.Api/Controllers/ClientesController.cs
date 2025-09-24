using EasyMoto.Api.Hypermedia;
using EasyMoto.Application.Clientes;
using EasyMoto.Application.Clientes.Contracts;
using EasyMoto.Application.Shared.Pagination;
using Microsoft.AspNetCore.Mvc;

namespace EasyMoto.Api.Controllers;

[ApiController]
[Route("api/clientes")]
public sealed class ClientesController : ControllerBase
{
    [HttpGet(Name = "GetClientes")]
    public async Task<IActionResult> Get([FromServices] ListarClientesHandler handler, [FromQuery] int page = 1, [FromQuery] int size = 10, CancellationToken ct = default)
    {
        var result = await handler.ExecuteAsync(new PageQuery(page, size), ct);
        var items = result.Items.Select(i => HateoasBuilder.WithLinks(i, new[]
        {
            new LinkDto { Rel = "self", Href = Url.Link("GetClienteById", new { id = i.Id }) ?? string.Empty, Method = "GET" },
            new LinkDto { Rel = "update", Href = Url.Link("UpdateCliente", new { id = i.Id }) ?? string.Empty, Method = "PUT" },
            new LinkDto { Rel = "delete", Href = Url.Link("DeleteCliente", new { id = i.Id }) ?? string.Empty, Method = "DELETE" }
        }));
        var links = HateoasBuilder.PagingLinks(Url, "GetClientes", result.Page, result.Size, result.HasPrevious, result.HasNext);
        return Ok(new { result.Page, result.Size, result.TotalCount, result.TotalPages, Items = items, _links = links });
    }

    [HttpGet("{id:guid}", Name = "GetClienteById")]
    public async Task<IActionResult> GetById([FromServices] ObterClientePorIdHandler handler, Guid id, CancellationToken ct = default)
    {
        var r = await handler.ExecuteAsync(id, ct);
        if (r is null) return NotFound();
        var res = HateoasBuilder.WithLinks(r, new[]
        {
            new LinkDto { Rel = "self", Href = Url.Link("GetClienteById", new { id = r.Id }) ?? string.Empty, Method = "GET" },
            new LinkDto { Rel = "update", Href = Url.Link("UpdateCliente", new { id = r.Id }) ?? string.Empty, Method = "PUT" },
            new LinkDto { Rel = "delete", Href = Url.Link("DeleteCliente", new { id = r.Id }) ?? string.Empty, Method = "DELETE" }
        });
        return Ok(res);
    }

    [HttpPost(Name = "CreateCliente")]
    public async Task<IActionResult> Post([FromServices] CriarClienteHandler handler, [FromBody] CriarClienteRequest request, CancellationToken ct = default)
    {
        var r = await handler.ExecuteAsync(request, ct);
        return CreatedAtRoute("GetClienteById", new { id = r.Id }, r);
    }

    [HttpPut("{id:guid}", Name = "UpdateCliente")]
    public async Task<IActionResult> Put([FromServices] AtualizarClienteHandler handler, Guid id, [FromBody] AtualizarClienteRequest request, CancellationToken ct = default)
    {
        var r = await handler.ExecuteAsync(id, request, ct);
        return r is null ? NotFound() : Ok(r);
    }

    [HttpDelete("{id:guid}", Name = "DeleteCliente")]
    public async Task<IActionResult> Delete([FromServices] ExcluirClienteHandler handler, Guid id, CancellationToken ct = default)
    {
        var ok = await handler.ExecuteAsync(id, ct);
        return ok ? NoContent() : NotFound();
    }
}
