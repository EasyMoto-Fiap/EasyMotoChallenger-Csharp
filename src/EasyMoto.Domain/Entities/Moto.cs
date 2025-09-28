namespace EasyMoto.Domain.Entities;

public class Moto : EntityBase
{
    public required string Placa { get; set; }
    public required string Modelo { get; set; }
    public int Ano { get; set; }
    public required string Cor { get; set; }
    public bool Ativo { get; set; } = true;
    public int FilialId { get; set; }
}