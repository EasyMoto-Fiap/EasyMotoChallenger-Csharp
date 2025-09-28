namespace EasyMoto.Application.DTOs.Usuarios;

public class UpdateUsuarioRequest
{
    public string NomeCompleto { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string? SenhaHash { get; set; }
    public string Telefone { get; set; } = default!;
    public string Cpf { get; set; } = default!;
    public string CepFilial { get; set; } = default!;
    public int Perfil { get; set; } = 0;
    public bool Ativo { get; set; } = true;
    public int FilialId { get; set; }
}