using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace EasyMoto.Api.Controllers;

[ApiController]
[Route("health")]
public sealed class HealthController : ControllerBase
{
    private readonly HealthCheckService _health;
    public HealthController(HealthCheckService health) => _health = health;

    [HttpGet]
    public async Task<IActionResult> Get(CancellationToken ct)
    {
        var report = await _health.CheckHealthAsync(_ => true, ct);
        var payload = new
        {
            status = report.Status.ToString(),
            totalDuration = report.TotalDuration.TotalMilliseconds,
            checks = report.Entries.Select(e => new
            {
                name = e.Key,
                status = e.Value.Status.ToString(),
                duration = e.Value.Duration.TotalMilliseconds,
                error = e.Value.Exception?.Message,
                data = e.Value.Data
            })
        };
        var code = report.Status == HealthStatus.Unhealthy ? 503 : 200;
        return StatusCode(code, payload);
    }
}