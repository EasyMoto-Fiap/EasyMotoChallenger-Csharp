using System.ComponentModel.DataAnnotations;

namespace EasyMoto.Application.DTOs.Notificacoes;

public class MarkAsLidaRequest
{
    [Required]
    public int UsuarioId { get; set; }
}