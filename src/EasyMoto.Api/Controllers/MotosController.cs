using Asp.Versioning;
using EasyMoto.Application.Motos;
using EasyMoto.Application.Motos.Contracts;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;
using EasyMoto.Api.Swagger.Examples.Motos;

namespace EasyMoto.Api.Controllers;

[ApiController]
[ApiVersion("1.0")]
[ApiVersion("2.0")]
[Produces("application/json")]
[Route("api/v{version:apiVersion}/motos")]
public sealed class MotosController : ControllerBase
{
    private readonly CriarMotoHandler _criar;
    private readonly ObterMotoPorIdHandler _obter;
    private readonly ListarMotosHandler _listar;
    private readonly AtualizarMotoHandler _atualizar;
    private readonly ExcluirMotoHandler _excluir;

    public MotosController(
        CriarMotoHandler criar,
        ObterMotoPorIdHandler obter,
        ListarMotosHandler listar,
        AtualizarMotoHandler atualizar,
        ExcluirMotoHandler excluir)
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
    [SwaggerOperation(Summary = "Cria uma moto")]
    [SwaggerRequestExample(typeof(CriarMotoRequest), typeof(CriarMotoRequestExample))]
    [SwaggerResponse(201, "Criado", typeof(MotoResponse))]
    [SwaggerResponseExample(201, typeof(MotoResponseExample))]
    [ProducesResponseType(typeof(MotoResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Criar([FromBody] CriarMotoRequest request, CancellationToken ct)
    {
        var result = await _criar.Handle(request, ct);
        return Created($"/api/v{HttpContext.GetRequestedApiVersion()}/motos/{result.IdMoto}", result);
    }

    [HttpGet("{id:int}")]
    [MapToApiVersion("1.0")]
    [MapToApiVersion("2.0")]
    [SwaggerOperation(Summary = "Obtém uma moto por id")]
    [SwaggerResponse(200, "OK", typeof(MotoResponse))]
    [SwaggerResponse(404, "Não encontrada")]
    [SwaggerResponseExample(200, typeof(MotoResponseExample))]
    [ProducesResponseType(typeof(MotoResponse), StatusCodes.Status200OK)]
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
    [SwaggerOperation(Summary = "Lista motos")]
    [SwaggerResponse(200, "OK", typeof(IEnumerable<MotoResponse>))]
    [SwaggerResponseExample(200, typeof(MotoListResponseExample))]
    [ProducesResponseType(typeof(IEnumerable<MotoResponse>), StatusCodes.Status200OK)]
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
    [SwaggerOperation(Summary = "Atualiza uma moto")]
    [SwaggerRequestExample(typeof(AtualizarMotoRequest), typeof(AtualizarMotoRequestExample))]
    [SwaggerResponse(200, "OK", typeof(MotoResponse))]
    [SwaggerResponse(404, "Não encontrada")]
    [SwaggerResponseExample(200, typeof(MotoResponseExample))]
    [ProducesResponseType(typeof(MotoResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Atualizar([FromRoute] int id, [FromBody] AtualizarMotoRequest request, CancellationToken ct)
    {
        var result = await _atualizar.Handle(id, request, ct);
        if (result is null) return NotFound();
        return Ok(result);
    }

    [HttpDelete("{id:int}")]
    [MapToApiVersion("1.0")]
    [MapToApiVersion("2.0")]
    [SwaggerOperation(Summary = "Exclui uma moto")]
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
