using Asp.Versioning;
using EasyMoto.Application.Clientes;
using EasyMoto.Application.Clientes.Contracts;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;
using EasyMoto.Api.Swagger.Examples.Clientes;

namespace EasyMoto.Api.Controllers;

[ApiController]
[ApiVersion("1.0")]
[ApiVersion("2.0")]
[Produces("application/json")]
[Route("api/v{version:apiVersion}/clientes")]
public sealed class ClientesController : ControllerBase
{
    private readonly CriarClienteHandler _criar;
    private readonly ObterClientePorIdHandler _obter;
    private readonly ListarClientesHandler _listar;
    private readonly AtualizarClienteHandler _atualizar;
    private readonly ExcluirClienteHandler _excluir;

    public ClientesController(
        CriarClienteHandler criar,
        ObterClientePorIdHandler obter,
        ListarClientesHandler listar,
        AtualizarClienteHandler atualizar,
        ExcluirClienteHandler excluir)
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
    [SwaggerOperation(Summary = "Cria um cliente")]
    [SwaggerRequestExample(typeof(CriarClienteRequest), typeof(CriarClienteRequestExample))]
    [SwaggerResponse(201, "Criado", typeof(ClienteResponse))]
    [SwaggerResponseExample(201, typeof(ClienteResponseExample))]
    [ProducesResponseType(typeof(ClienteResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Criar([FromBody] CriarClienteRequest request, CancellationToken ct)
    {
        var result = await _criar.Handle(request, ct);
        return Created($"/api/v{HttpContext.GetRequestedApiVersion()}/clientes/{result.IdCliente}", result);
    }

    [HttpGet("{id:int}")]
    [MapToApiVersion("1.0")]
    [MapToApiVersion("2.0")]
    [SwaggerOperation(Summary = "Obtém um cliente por id")]
    [SwaggerResponse(200, "OK", typeof(ClienteResponse))]
    [SwaggerResponse(404, "Não encontrado")]
    [SwaggerResponseExample(200, typeof(ClienteResponseExample))]
    [ProducesResponseType(typeof(ClienteResponse), StatusCodes.Status200OK)]
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
    [SwaggerOperation(Summary = "Lista clientes")]
    [SwaggerResponse(200, "OK", typeof(IEnumerable<ClienteResponse>))]
    [SwaggerResponseExample(200, typeof(ClienteListResponseExample))]
    [ProducesResponseType(typeof(IEnumerable<ClienteResponse>), StatusCodes.Status200OK)]
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
    [SwaggerOperation(Summary = "Atualiza um cliente")]
    [SwaggerRequestExample(typeof(AtualizarClienteRequest), typeof(AtualizarClienteRequestExample))]
    [SwaggerResponse(200, "OK", typeof(ClienteResponse))]
    [SwaggerResponse(404, "Não encontrado")]
    [SwaggerResponseExample(200, typeof(ClienteResponseExample))]
    [ProducesResponseType(typeof(ClienteResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Atualizar([FromRoute] int id, [FromBody] AtualizarClienteRequest request, CancellationToken ct)
    {
        var result = await _atualizar.Handle(id, request, ct);
        if (result is null) return NotFound();
        return Ok(result);
    }

    [HttpDelete("{id:int}")]
    [MapToApiVersion("1.0")]
    [MapToApiVersion("2.0")]
    [SwaggerOperation(Summary = "Exclui um cliente")]
    [SwaggerResponse(204, "Sem conteúdo")]
    [SwaggerResponse(404, "Não encontrado")]
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
