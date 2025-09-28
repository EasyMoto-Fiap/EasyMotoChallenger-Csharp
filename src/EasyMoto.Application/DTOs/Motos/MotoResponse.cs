namespace EasyMoto.Application.DTOs.Motos;

public class MotoResponse
{
    public int Id { get; set; }
    public string Placa { get; set; } = default!;
    public string Modelo { get; set; } = default!;
    public int Ano { get; set; }
    public string Cor { get; set; } = default!;
    public bool Ativo { get; set; }
    public int FilialId { get; set; }
    public int Categoria { get; set; }
    public int StatusOperacional { get; set; }
    public int? LegendaStatusId { get; set; }
    public string? QrCode { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public IDictionary<string, string>? Links { get; set; }
}