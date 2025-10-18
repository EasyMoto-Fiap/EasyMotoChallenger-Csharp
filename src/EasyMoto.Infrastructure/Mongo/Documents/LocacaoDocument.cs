using EasyMoto.Infrastructure.Mongo.Serializers;
using MongoDB.Bson.Serialization.Attributes;

namespace EasyMoto.Infrastructure.Mongo.Documents;

[BsonIgnoreExtraElements(true)]
public class LocacaoDocument
{
    [BsonElement("IdLocacao")]
    public int IdLocacao { get; set; }

    [BsonElement("ClienteId")]
    public int ClienteId { get; set; }

    [BsonElement("DataInicio")]
    public DateTime DataInicio { get; set; }

    [BsonElement("DataFim")]
    public DateTime? DataFim { get; set; }

    [BsonElement("StatusLocacao")]
    [BsonSerializer(typeof(FlexibleStringOrIntSerializer))]
    public string StatusLocacao { get; set; } = null!;
}