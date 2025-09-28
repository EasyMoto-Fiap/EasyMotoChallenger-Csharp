using System.ComponentModel.DataAnnotations;

namespace EasyMoto.Application.DTOs.Legendas;

public class UpdateLegendaStatusRequest
{
    [Required]
    public required string Titulo { get; set; }
    public string? Descricao { get; set; }
    public string? CorHex { get; set; }
    public bool Ativo { get; set; } = true;
}