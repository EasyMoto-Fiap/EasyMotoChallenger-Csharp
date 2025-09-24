using EasyMoto.Api.Hypermedia;
using EasyMoto.Application.Motos;
using EasyMoto.Application.Motos.Contracts;
using EasyMoto.Application.Shared.Pagination;
using Microsoft.AspNetCore.Mvc;

namespace EasyMoto.Api.Controllers;

[ApiController]
[Route("api/motos")]
public sealed class MotosController : ControllerBase
{
    [HttpGet(Name = "GetMotos")]
    public async Task<IActionResult> Get([FromServices] ListarMotosHandler handler, [FromQuery] int page = 1, [FromQuery] int size = 10, CancellationToken ct = default)
    {
        var result = await handler.ExecuteAsync(new PageQuery(page, size), ct);
        var items = result.Items.Select(i => HateoasBuilder.WithLinks(i, new[]
        {
            new LinkDto { Rel = "self", Href = Url.Link("GetMotoById", new { id = i.Id }) ?? string.Empty, Method = "GET" },
            new LinkDto { Rel = "update", Href = Url.Link("UpdateMoto", new { id = i.Id }) ?? string.Empty, Method = "PUT" },
            new LinkDto { Rel = "delete", Href = Url.Link("DeleteMoto", new { id = i.Id }) ?? string.Empty, Method = "DELETE" }
        }));
        var links = HateoasBuilder.PagingLinks(Url, "GetMotos", result.Page, result.Size, result.HasPrevious, result.HasNext);
        return Ok(new { result.Page, result.Size, result.TotalCount, result.TotalPages, Items = items, _links = links });
    }

    [HttpGet("{id:guid}", Name = "GetMotoById")]
    public async Task<IActionResult> GetById([FromServices] ObterMotoPorIdHandler handler, Guid id, CancellationToken ct = default)
    {
        var r = await handler.ExecuteAsync(id, ct);
        if (r is null) return NotFound();
        var res = HateoasBuilder.WithLinks(r, new[]
        {
            new LinkDto { Rel = "self", Href = Url.Link("GetMotoById", new { id = r.Id }) ?? string.Empty, Method = "GET" },
            new LinkDto { Rel = "update", Href = Url.Link("UpdateMoto", new { id = r.Id }) ?? string.Empty, Method = "PUT" },
            new LinkDto { Rel = "delete", Href = Url.Link("DeleteMoto", new { id = r.Id }) ?? string.Empty, Method = "DELETE" }
        });
        return Ok(res);
    }

    [HttpPost(Name = "CreateMoto")]
    public async Task<IActionResult> Post([FromServices] CriarMotoHandler handler, [FromBody] CriarMotoRequest request, CancellationToken ct = default)
    {
        var r = await handler.ExecuteAsync(request, ct);
        return CreatedAtRoute("GetMotoById", new { id = r.Id }, r);
    }

    [HttpPut("{id:guid}", Name = "UpdateMoto")]
    public async Task<IActionResult> Put([FromServices] AtualizarMotoHandler handler, Guid id, [FromBody] AtualizarMotoRequest request, CancellationToken ct = default)
    {
        var r = await handler.ExecuteAsync(id, request, ct);
        return r is null ? NotFound() : Ok(r);
    }

    [HttpDelete("{id:guid}", Name = "DeleteMoto")]
    public async Task<IActionResult> Delete([FromServices] ExcluirMotoHandler handler, Guid id, CancellationToken ct = default)
    {
        var ok = await handler.ExecuteAsync(id, ct);
        return ok ? NoContent() : NotFound();
    }
}
