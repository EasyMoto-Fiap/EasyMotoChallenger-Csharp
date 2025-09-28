namespace EasyMoto.Application.DTOs.Legendas;

public class LegendaStatusResponse
{
    public int Id { get; set; }
    public string Titulo { get; set; } = default!;
    public string? Descricao { get; set; }
    public string? CorHex { get; set; }
    public bool Ativo { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public IDictionary<string, string>? Links { get; set; }
}