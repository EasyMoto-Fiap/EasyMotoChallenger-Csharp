using MongoDB.Bson.Serialization.Attributes;

namespace EasyMoto.Infrastructure.Mongo.Documents;

public class MotoDocument
{
    [BsonId]
    public int IdMoto { get; set; }
    public string ModeloMoto { get; set; } = null!;
    public string PlacaMoto { get; set; } = null!;
    public int AnoMoto { get; set; }
    public int StatusMoto { get; set; }
}