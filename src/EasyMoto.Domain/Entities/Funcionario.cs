namespace EasyMoto.Domain.Entities;

public sealed class Funcionario
{
    public int Id { get; private set; }
    public string NomeFuncionario { get; private set; } = string.Empty;
    public string Cpf { get; private set; } = string.Empty;
    public int FilialId { get; private set; }
    public Filial? Filial { get; private set; }

    public Funcionario() { }

    public Funcionario(string nomeFuncionario, string cpf, int filialId)
    {
        NomeFuncionario = nomeFuncionario;
        Cpf = cpf;
        FilialId = filialId;
    }

    public void Atualizar(string nomeFuncionario, string cpf, int filialId)
    {
        NomeFuncionario = nomeFuncionario;
        Cpf = cpf;
        FilialId = filialId;
    }
}