using System.ComponentModel.DataAnnotations;

namespace EasyMoto.Application.DTOs.Usuarios
{
    public class CreateUsuarioRequest
    {
        [Required]
        public string NomeCompleto { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Telefone { get; set; }

        [Required]
        public string Cpf { get; set; }

        [Required]
        public string CepFilial { get; set; }

        [Required]
        public int Perfil { get; set; }

        [Required]
        public bool Ativo { get; set; }

        [Required]
        public int FilialId { get; set; }

        [Required]
        public string Senha { get; set; }

        [Required]
        public string ConfirmarSenha { get; set; }

        public string? SenhaHash { get; set; }
    }
}