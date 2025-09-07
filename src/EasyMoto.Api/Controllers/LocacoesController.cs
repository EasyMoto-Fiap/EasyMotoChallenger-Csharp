using EasyMoto.Application.Locacoes;
using EasyMoto.Application.Locacoes.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace EasyMoto.Api.Controllers;

[ApiController]
[Route("api/locacoes")]
public sealed class LocacoesController : ControllerBase
{
    private readonly CriarLocacaoHandler _criar;
    private readonly ObterLocacaoPorIdHandler _obter;
    private readonly ListarLocacoesHandler _listar;
    private readonly AtualizarLocacaoHandler _atualizar;
    private readonly ExcluirLocacaoHandler _excluir;

    public LocacoesController(
        CriarLocacaoHandler criar,
        ObterLocacaoPorIdHandler obter,
        ListarLocacoesHandler listar,
        AtualizarLocacaoHandler atualizar,
        ExcluirLocacaoHandler excluir)
    {
        _criar = criar;
        _obter = obter;
        _listar = listar;
        _atualizar = atualizar;
        _excluir = excluir;
    }

    [HttpPost]
    public async Task<IActionResult> Criar([FromBody] CriarLocacaoRequest request, CancellationToken ct)
    {
        var result = await _criar.Handle(request, ct);
        return Created($"/api/locacoes/{result.IdLocacao}", result);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> ObterPorId([FromRoute] int id, CancellationToken ct)
    {
        var result = await _obter.Handle(id, ct);
        if (result is null) return NotFound();
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> Listar(CancellationToken ct)
    {
        var result = await _listar.Handle(ct);
        return Ok(result);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Atualizar([FromRoute] int id, [FromBody] AtualizarLocacaoRequest request, CancellationToken ct)
    {
        var result = await _atualizar.Handle(id, request, ct);
        if (result is null) return NotFound();
        return Ok(result);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Excluir([FromRoute] int id, CancellationToken ct)
    {
        var ok = await _excluir.Handle(id, ct);
        if (!ok) return NotFound();
        return NoContent();
    }
}
