using EasyMoto.Api.Hypermedia;
using EasyMoto.Application.Locacoes;
using EasyMoto.Application.Locacoes.Contracts;
using EasyMoto.Application.Shared.Pagination;
using Microsoft.AspNetCore.Mvc;

namespace EasyMoto.Api.Controllers;

[ApiController]
[Route("api/locacoes")]
public sealed class LocacoesController : ControllerBase
{
    [HttpGet(Name = "GetLocacoes")]
    public async Task<IActionResult> Get([FromServices] ListarLocacoesHandler handler, [FromQuery] int page = 1, [FromQuery] int size = 10, CancellationToken ct = default)
    {
        var result = await handler.ExecuteAsync(new PageQuery(page, size), ct);
        var items = result.Items.Select(i => HateoasBuilder.WithLinks(i, new[]
        {
            new LinkDto { Rel = "self", Href = Url.Link("GetLocacaoById", new { id = i.Id }) ?? string.Empty, Method = "GET" },
            new LinkDto { Rel = "update", Href = Url.Link("UpdateLocacao", new { id = i.Id }) ?? string.Empty, Method = "PUT" },
            new LinkDto { Rel = "delete", Href = Url.Link("DeleteLocacao", new { id = i.Id }) ?? string.Empty, Method = "DELETE" }
        }));
        var links = HateoasBuilder.PagingLinks(Url, "GetLocacoes", result.Page, result.Size, result.HasPrevious, result.HasNext);
        return Ok(new { result.Page, result.Size, result.TotalCount, result.TotalPages, Items = items, _links = links });
    }

    [HttpGet("{id:guid}", Name = "GetLocacaoById")]
    public async Task<IActionResult> GetById([FromServices] ObterLocacaoPorIdHandler handler, Guid id, CancellationToken ct = default)
    {
        var r = await handler.ExecuteAsync(id, ct);
        if (r is null) return NotFound();
        var res = HateoasBuilder.WithLinks(r, new[]
        {
            new LinkDto { Rel = "self", Href = Url.Link("GetLocacaoById", new { id = r.Id }) ?? string.Empty, Method = "GET" },
            new LinkDto { Rel = "update", Href = Url.Link("UpdateLocacao", new { id = r.Id }) ?? string.Empty, Method = "PUT" },
            new LinkDto { Rel = "delete", Href = Url.Link("DeleteLocacao", new { id = r.Id }) ?? string.Empty, Method = "DELETE" }
        });
        return Ok(res);
    }

    [HttpPost(Name = "CreateLocacao")]
    public async Task<IActionResult> Post([FromServices] CriarLocacaoHandler handler, [FromBody] CriarLocacaoRequest request, CancellationToken ct = default)
    {
        var r = await handler.ExecuteAsync(request, ct);
        return CreatedAtRoute("GetLocacaoById", new { id = r.Id }, r);
    }

    [HttpPut("{id:guid}", Name = "UpdateLocacao")]
    public async Task<IActionResult> Put([FromServices] AtualizarLocacaoHandler handler, Guid id, [FromBody] AtualizarLocacaoRequest request, CancellationToken ct = default)
    {
        var r = await handler.ExecuteAsync(id, request, ct);
        return r is null ? NotFound() : Ok(r);
    }

    [HttpDelete("{id:guid}", Name = "DeleteLocacao")]
    public async Task<IActionResult> Delete([FromServices] ExcluirLocacaoHandler handler, Guid id, CancellationToken ct = default)
    {
        var ok = await handler.ExecuteAsync(id, ct);
        return ok ? NoContent() : NotFound();
    }
}
