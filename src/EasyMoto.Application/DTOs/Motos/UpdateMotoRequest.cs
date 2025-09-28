using System.ComponentModel.DataAnnotations;

namespace EasyMoto.Application.DTOs.Motos;

public class UpdateMotoRequest
{
    [Required]
    public required string Placa { get; set; }
    [Required]
    public required string Modelo { get; set; }
    [Range(1900, 2100)]
    public int Ano { get; set; }
    [Required]
    public required string Cor { get; set; }
    public bool Ativo { get; set; } = true;
    [Required]
    public int FilialId { get; set; }
    [Range(0, 2)]
    public int Categoria { get; set; } = 0;
    [Range(0, 2)]
    public int StatusOperacional { get; set; } = 0;
    public int? LegendaStatusId { get; set; }
    public string? QrCode { get; set; }
}