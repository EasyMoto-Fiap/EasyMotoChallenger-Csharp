namespace EasyMoto.Application.Vagas.Contracts;

public sealed class VagaResponse
{
    public int Id { get; set; }
    public int PatioId { get; set; }
    public string NumeroVaga { get; set; } = string.Empty;
    public bool Ocupada { get; set; }
    public int? MotoId { get; set; }
}