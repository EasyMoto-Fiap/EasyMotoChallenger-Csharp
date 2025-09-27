namespace EasyMoto.Application.Funcionarios.Contracts;

public sealed class FuncionarioResponse
{
    public int Id { get; set; }
    public string NomeFuncionario { get; set; } = string.Empty;
    public string Cpf { get; set; } = string.Empty;
    public int FilialId { get; set; }
}