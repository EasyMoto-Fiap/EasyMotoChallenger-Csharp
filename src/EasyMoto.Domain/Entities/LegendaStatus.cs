namespace EasyMoto.Domain.Entities;

public class LegendaStatus : EntityBase
{
    public required string Titulo { get; set; }
    public string? Descricao { get; set; }
    public string? CorHex { get; set; }
    public bool Ativo { get; set; } = true;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}