using System.ComponentModel.DataAnnotations;

namespace EasyMoto.Application.DTOs.Usuarios;

public class CreateUsuarioRequest
{
    [Required]
    public required string NomeCompleto { get; set; }

    [Required, EmailAddress]
    public required string Email { get; set; }

    [Required]
    public required string Telefone { get; set; }

    [Required]
    public required string Cpf { get; set; }

    [Required]
    public required string CepFilial { get; set; }

    [Required, MinLength(6)]
    public required string Senha { get; set; }

    [Required, Compare(nameof(Senha))]
    public required string ConfirmarSenha { get; set; }

    public int Perfil { get; set; } = 0;

    public bool Ativo { get; set; } = true;

    [Required]
    public int FilialId { get; set; }
}