namespace EasyMoto.Domain.Entities
{
    public sealed class Cliente
    {
        public int Id { get; private set; }
        public string NomeCliente { get; private set; } = string.Empty;
        public string CpfCliente { get; private set; } = string.Empty;
        public string TelefoneCliente { get; private set; } = string.Empty;
        public string EmailCliente { get; private set; } = string.Empty;

        public ICollection<ClienteLocacao> Locacoes { get; private set; } = new List<ClienteLocacao>();

        public Cliente() { }

        public Cliente(string nome, string cpf, string telefone, string email)
        {
            Update(nome, cpf, telefone, email);
        }

        public void Update(string nome, string cpf, string telefone, string email)
        {
            NomeCliente = nome;
            CpfCliente = cpf;
            TelefoneCliente = telefone;
            EmailCliente = email;
        }
    }
}