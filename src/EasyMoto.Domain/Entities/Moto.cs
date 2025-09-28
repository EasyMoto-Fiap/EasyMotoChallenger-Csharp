namespace EasyMoto.Domain.Entities;

public enum CategoriaMoto
{
    Pop = 0,
    Sport = 1,
    E = 2
}

public enum StatusOperacionalMoto
{
    Disponivel = 0,
    Alugada = 1,
    Manutencao = 2
}

public class Moto : EntityBase
{
    public required string Placa { get; set; }
    public required string Modelo { get; set; }
    public int Ano { get; set; }
    public required string Cor { get; set; }
    public bool Ativo { get; set; } = true;
    public int FilialId { get; set; }
    public CategoriaMoto Categoria { get; set; } = CategoriaMoto.Pop;
    public StatusOperacionalMoto StatusOperacional { get; set; } = StatusOperacionalMoto.Disponivel;
    public int? LegendaStatusId { get; set; }
    public string? QrCode { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}