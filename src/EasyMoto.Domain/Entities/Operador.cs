namespace EasyMoto.Domain.Entities;

public class Operador
{
    public int Id { get; private set; }
    public string NomeOperador { get; private set; }
    public string Cpf { get; private set; }
    public string Telefone { get; private set; }
    public string Email { get; private set; }
    public int FilialId { get; private set; }

    public Operador(string nomeOperador, string cpf, string telefone, string email, int filialId)
    {
        NomeOperador = nomeOperador;
        Cpf = cpf;
        Telefone = telefone;
        Email = email;
        FilialId = filialId;
    }

    public void Update(string nomeOperador, string cpf, string telefone, string email, int filialId)
    {
        NomeOperador = nomeOperador;
        Cpf = cpf;
        Telefone = telefone;
        Email = email;
        FilialId = filialId;
    }
}