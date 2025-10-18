using MongoDB.Bson.Serialization.Attributes;

namespace EasyMoto.Infrastructure.Mongo.Documents;

public class ClienteDocument
{
    [BsonId]
    public int IdCliente { get; set; }
    public string NomeCliente { get; set; } = null!;
    public string CpfCliente { get; set; } = null!;
    public string TelefoneCliente { get; set; } = null!;
    public string EmailCliente { get; set; } = null!;
}