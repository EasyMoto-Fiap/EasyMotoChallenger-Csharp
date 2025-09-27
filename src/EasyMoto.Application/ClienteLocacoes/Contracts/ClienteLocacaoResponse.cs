namespace EasyMoto.Application.ClienteLocacoes.Contracts;

public sealed class ClienteLocacaoResponse
{
    public int Id { get; set; }
    public int ClienteId { get; set; }
    public int MotoId { get; set; }
    public DateTime DataInicio { get; set; }
    public DateTime? DataFim { get; set; }
    public string StatusLocacao { get; set; } = string.Empty;
}