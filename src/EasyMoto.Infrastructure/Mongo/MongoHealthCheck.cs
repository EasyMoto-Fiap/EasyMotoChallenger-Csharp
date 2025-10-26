using Microsoft.Extensions.Diagnostics.HealthChecks;
using MongoDB.Bson;
using MongoDB.Driver;

namespace EasyMoto.Infrastructure.Mongo;

public sealed class MongoHealthCheck : IHealthCheck
{
    private readonly IMongoDatabase _db;
    public MongoHealthCheck(IMongoDatabase db) => _db = db;
    public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        try
        {
            await _db.RunCommandAsync<BsonDocument>(new BsonDocument("ping", 1), cancellationToken: cancellationToken);
            return HealthCheckResult.Healthy("Mongo reachable");
        }
        catch (Exception ex)
        {
            return HealthCheckResult.Unhealthy("Mongo unreachable", ex);
        }
    }
}