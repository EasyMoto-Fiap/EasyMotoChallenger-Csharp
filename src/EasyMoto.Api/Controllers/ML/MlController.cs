using EasyMoto.Api.ML;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace EasyMoto.Api.Controllers.ML;

[ApiController]
[ApiVersion("1.0")]
[ApiVersion("2.0")]
[Route("api/[controller]")]
public sealed class MlController : ControllerBase
{
    readonly IMaintenancePredictionService _svc;

    public MlController(IMaintenancePredictionService svc)
    {
        _svc = svc;
    }

    [HttpPost("maintenance")]
    [SwaggerOperation(Summary = "Predição de manutenção", Description = "Retorna se é recomendada manutenção preventiva para a moto.")]
    [ProducesResponseType(typeof(PredictMaintenanceResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<PredictMaintenanceResponse> PredictMaintenance([FromBody] PredictMaintenanceRequest request)
    {
        if (request.Ano <= 0 || request.MesesDesdeUltimaRevisao < 0 || request.KmDesdeUltimaRevisao < 0)
            return BadRequest(new { message = "Dados inválidos." });

        var result = _svc.Predict(request);
        return Ok(result);
    }
}