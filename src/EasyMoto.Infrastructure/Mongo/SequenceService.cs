using MongoDB.Bson;
using MongoDB.Driver;

namespace EasyMoto.Infrastructure.Mongo;

public class SequenceService
{
    private readonly IMongoCollection<BsonDocument> _col;

    public SequenceService(MongoDbContext ctx)
    {
        _col = ctx.Database.GetCollection<BsonDocument>("_sequences");
    }

    public async Task<long> NextAsync(string name)
    {
        var filter = Builders<BsonDocument>.Filter.Eq("_id", name);
        var update = Builders<BsonDocument>.Update.Inc("value", 1);
        var options = new FindOneAndUpdateOptions<BsonDocument>
        {
            IsUpsert = true,
            ReturnDocument = ReturnDocument.After
        };
        var doc = await _col.FindOneAndUpdateAsync(filter, update, options);
        return doc.GetValue("value", 1).ToInt64();
    }
}