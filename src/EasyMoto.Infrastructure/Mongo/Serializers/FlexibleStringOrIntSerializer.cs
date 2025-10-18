using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace EasyMoto.Infrastructure.Mongo.Serializers;

public sealed class FlexibleStringOrIntSerializer : SerializerBase<string>
{
    public override string Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
    {
        var reader = context.Reader;
        var t = reader.CurrentBsonType;
        if (t == BsonType.String) return reader.ReadString();
        if (t == BsonType.Int32) return reader.ReadInt32().ToString();
        if (t == BsonType.Int64) return reader.ReadInt64().ToString();
        if (t == BsonType.Null) { reader.ReadNull(); return string.Empty; }
        var v = BsonValueSerializer.Instance.Deserialize(context, args) as BsonValue;
        return v?.ToString() ?? string.Empty;
    }

    public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args, string value)
    {
        context.Writer.WriteString(value);
    }
}