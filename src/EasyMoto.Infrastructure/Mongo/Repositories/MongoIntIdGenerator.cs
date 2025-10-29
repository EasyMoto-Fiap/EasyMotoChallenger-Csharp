using MongoDB.Bson;
using MongoDB.Driver;

namespace EasyMoto.Infrastructure.Mongo.Repositories;

public sealed class MongoIntIdGenerator
{
    private readonly IMongoCollection<BsonDocument> _counters;
    public MongoIntIdGenerator(IMongoDatabase db) => _counters = db.GetCollection<BsonDocument>("counters");

    public async Task<int> NextAsync(string key, CancellationToken ct = default)
    {
        var filter = Builders<BsonDocument>.Filter.Eq("_id", key);
        var update = Builders<BsonDocument>.Update.Inc("seq", 1);
        var opts = new FindOneAndUpdateOptions<BsonDocument, BsonDocument> { IsUpsert = true, ReturnDocument = ReturnDocument.After };
        var doc = await _counters.FindOneAndUpdateAsync(filter, update, opts, ct);
        return doc["seq"].AsInt32;
    }

    public Task<int> NextForAsync<T>(CancellationToken ct = default) => NextAsync(typeof(T).Name, ct);
}