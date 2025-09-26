namespace EasyMoto.Domain.Entities;

public sealed class Operador
{
    public int IdOperador { get; private set; }
    public int Id => IdOperador;
    public string NomeOperador { get; private set; } = null!;
    public string Cpf { get; private set; } = null!;
    public string Telefone { get; private set; } = null!;
    public string Email { get; private set; } = null!;
    public int FilialId { get; private set; }
    public Filial? Filial { get; private set; }

    public Operador() { }

    public Operador(string nomeOperador, string cpf, string telefone, string email, int filialId)
    {
        Update(nomeOperador, cpf, telefone, email);
        FilialId = filialId;
    }

    public void Update(string nomeOperador, string cpf, string telefone, string email)
    {
        if (string.IsNullOrWhiteSpace(nomeOperador)) throw new ArgumentException(nameof(nomeOperador));
        if (string.IsNullOrWhiteSpace(cpf)) throw new ArgumentException(nameof(cpf));
        if (string.IsNullOrWhiteSpace(telefone)) throw new ArgumentException(nameof(telefone));
        if (string.IsNullOrWhiteSpace(email)) throw new ArgumentException(nameof(email));
        NomeOperador = nomeOperador.Trim();
        Cpf = cpf.Trim();
        Telefone = telefone.Trim();
        Email = email.Trim();
    }

    public void Update(string nomeOperador, string cpf, string telefone, string email, int filialId)
    {
        Update(nomeOperador, cpf, telefone, email);
        FilialId = filialId;
    }

    public void AlterarFilial(int filialId)
    {
        if (filialId <= 0) throw new ArgumentException(nameof(filialId));
        FilialId = filialId;
    }
}