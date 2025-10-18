
using Microsoft.Extensions.Diagnostics.HealthChecks; using MongoDB.Bson;


namespace EasyMoto.Infrastructure.Mongo;

public class MongoHealthCheck : IHealthCheck
{
    private readonly MongoDbContext _ctx;

    public MongoHealthCheck(MongoDbContext ctx) => _ctx = ctx;

    public async Task<HealthCheckResult> CheckHealthAsync(
        HealthCheckContext context,
        CancellationToken cancellationToken = default)
    {
        try
        {
            await _ctx.Database.RunCommandAsync<BsonDocument>(new BsonDocument("ping", 1), cancellationToken: cancellationToken);
            return HealthCheckResult.Healthy("ok");
        }
        catch (System.Exception ex)
        {
            return HealthCheckResult.Unhealthy("fail", ex);
        }
    }
}