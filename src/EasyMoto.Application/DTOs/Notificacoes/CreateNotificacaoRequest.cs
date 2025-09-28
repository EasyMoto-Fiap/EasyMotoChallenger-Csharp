using System.ComponentModel.DataAnnotations;

namespace EasyMoto.Application.DTOs.Notificacoes;

public class CreateNotificacaoRequest
{
    [Range(0, 2)]
    public int Tipo { get; set; }
    [Required]
    public required string Mensagem { get; set; }
    public int? MotoId { get; set; }
    [Required]
    public int UsuarioOrigemId { get; set; }
    [Range(0, 2)]
    public int Escopo { get; set; } = 0;
}