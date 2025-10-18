using Asp.Versioning;
using EasyMoto.Application.Locacoes;
using EasyMoto.Application.Locacoes.Contracts;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;
using EasyMoto.Api.Swagger.Examples.Locacoes;

namespace EasyMoto.Api.Controllers;

[ApiController]
[ApiVersion("1.0")]
[ApiVersion("2.0")]
[Produces("application/json")]
[Route("api/v{version:apiVersion}/locacoes")]
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
    [Consumes("application/json")]
    [MapToApiVersion("1.0")]
    [MapToApiVersion("2.0")]
    [SwaggerOperation(Summary = "Cria uma locação")]
    [SwaggerRequestExample(typeof(CriarLocacaoRequest), typeof(CriarLocacaoRequestExample))]
    [SwaggerResponse(201, "Criado", typeof(LocacaoResponse))]
    [SwaggerResponseExample(201, typeof(LocacaoResponseExample))]
    [ProducesResponseType(typeof(LocacaoResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Criar([FromBody] CriarLocacaoRequest request, CancellationToken ct)
    {
        var result = await _criar.Handle(request, ct);
        return Created($"/api/v{HttpContext.GetRequestedApiVersion()}/locacoes/{result.IdLocacao}", result);
    }

    [HttpGet("{id:int}")]
    [MapToApiVersion("1.0")]
    [MapToApiVersion("2.0")]
    [SwaggerOperation(Summary = "Obtém uma locação por id")]
    [SwaggerResponse(200, "OK", typeof(LocacaoResponse))]
    [SwaggerResponse(404, "Não encontrada")]
    [SwaggerResponseExample(200, typeof(LocacaoResponseExample))]
    [ProducesResponseType(typeof(LocacaoResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> ObterPorId([FromRoute] int id, CancellationToken ct)
    {
        var result = await _obter.Handle(id, ct);
        if (result is null) return NotFound();
        return Ok(result);
    }

    [HttpGet]
    [MapToApiVersion("1.0")]
    [MapToApiVersion("2.0")]
    [SwaggerOperation(Summary = "Lista locações")]
    [SwaggerResponse(200, "OK", typeof(IEnumerable<LocacaoResponse>))]
    [SwaggerResponseExample(200, typeof(LocacaoListResponseExample))]
    [ProducesResponseType(typeof(IEnumerable<LocacaoResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Listar(CancellationToken ct)
    {
        var result = await _listar.Handle(ct);
        return Ok(result);
    }

    [HttpPut("{id:int}")]
    [Consumes("application/json")]
    [MapToApiVersion("1.0")]
    [MapToApiVersion("2.0")]
    [SwaggerOperation(Summary = "Atualiza uma locação")]
    [SwaggerRequestExample(typeof(AtualizarLocacaoRequest), typeof(AtualizarLocacaoRequestExample))]
    [SwaggerResponse(200, "OK", typeof(LocacaoResponse))]
    [SwaggerResponse(404, "Não encontrada")]
    [SwaggerResponseExample(200, typeof(LocacaoResponseExample))]
    [ProducesResponseType(typeof(LocacaoResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Atualizar([FromRoute] int id, [FromBody] AtualizarLocacaoRequest request, CancellationToken ct)
    {
        var result = await _atualizar.Handle(id, request, ct);
        if (result is null) return NotFound();
        return Ok(result);
    }

    [HttpDelete("{id:int}")]
    [MapToApiVersion("1.0")]
    [MapToApiVersion("2.0")]
    [SwaggerOperation(Summary = "Exclui uma locação")]
    [SwaggerResponse(204, "Sem conteúdo")]
    [SwaggerResponse(404, "Não encontrada")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Excluir([FromRoute] int id, CancellationToken ct)
    {
        var ok = await _excluir.Handle(id, ct);
        if (!ok) return NotFound();
        return NoContent();
    }
}
