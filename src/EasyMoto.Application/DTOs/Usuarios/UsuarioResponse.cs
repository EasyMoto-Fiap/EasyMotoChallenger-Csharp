namespace EasyMoto.Application.DTOs.Usuarios;

public class UsuarioResponse
{
    public int Id { get; set; }
    public string NomeCompleto { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string Telefone { get; set; } = default!;
    public string Cpf { get; set; } = default!;
    public string CepFilial { get; set; } = default!;
    public int Perfil { get; set; }
    public bool Ativo { get; set; }
    public int FilialId { get; set; }
    public IDictionary<string, string>? Links { get; set; }
}