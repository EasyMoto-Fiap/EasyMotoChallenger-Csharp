using EasyMoto.Application.Motos;
using EasyMoto.Application.Motos.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace EasyMoto.Api.Controllers;

[ApiController]
[Route("api/motos")]
public sealed class MotosController : ControllerBase
{
    private readonly CriarMotoHandler _criar;

    public MotosController(CriarMotoHandler criar) => _criar = criar;

    [HttpPost]
    public async Task<IActionResult> Criar([FromBody] CriarMotoRequest request, CancellationToken ct)
    {
        var result = await _criar.Handle(request, ct);
        return Created($"/api/motos/{result.IdMoto}", result);
    }
}
