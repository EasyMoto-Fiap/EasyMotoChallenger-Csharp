namespace EasyMoto.Domain.Entities;

public enum PerfilUsuario
{
    Operador = 0,
    Admin = 1
}

public class Usuario : EntityBase
{
    public required string NomeCompleto { get; set; }
    public required string Email { get; set; }
    public required string SenhaHash { get; set; }
    public required string Telefone { get; set; }
    public required string Cpf { get; set; }
    public required string CepFilial { get; set; }
    public PerfilUsuario Perfil { get; set; } = PerfilUsuario.Operador;
    public bool Ativo { get; set; } = true;
    public int FilialId { get; set; }
}