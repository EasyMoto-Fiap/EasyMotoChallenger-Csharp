using EasyMoto.Application.Clientes;
using EasyMoto.Application.Clientes.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace EasyMoto.Api.Controllers;

[ApiController]
[Route("api/clientes")]
public sealed class ClientesController : ControllerBase
{
    private readonly CriarClienteHandler _criar;

    public ClientesController(CriarClienteHandler criar) => _criar = criar;

    [HttpPost]
    public async Task<IActionResult> Criar([FromBody] CriarClienteRequest request, CancellationToken ct)
    {
        var result = await _criar.Handle(request, ct);
        return Created($"/api/clientes/{result.IdCliente}", result);
    }
}
