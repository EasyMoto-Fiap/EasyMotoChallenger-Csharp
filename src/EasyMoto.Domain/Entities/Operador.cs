namespace EasyMoto.Domain.Entities
{
    public class Operador
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public string NomeOperador { get; private set; }
        public string Cpf { get; private set; }
        public string Telefone { get; private set; }
        public string Email { get; private set; }
        public Guid FilialId { get; private set; }
        public Filial? Filial { get; private set; }

        private Operador() { }

        public Operador(string nomeOperador, string cpf, string telefone, string email, Guid filialId)
        {
            NomeOperador = nomeOperador;
            Cpf = cpf;
            Telefone = telefone;
            Email = email;
            FilialId = filialId;
        }

        public void Update(string nomeOperador, string cpf, string telefone, string email, Guid filialId)
        {
            NomeOperador = nomeOperador;
            Cpf = cpf;
            Telefone = telefone;
            Email = email;
            FilialId = filialId;
        }
    }
}