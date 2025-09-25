using System;

namespace EasyMoto.Domain.Entities
{
    public sealed class Funcionario
    {
        public Guid Id { get; private set; }
        public string NomeFuncionario { get; private set; } = string.Empty;
        public string Cpf { get; private set; } = string.Empty;
        public int FilialId { get; private set; }
        public Filial? Filial { get; private set; }

        private Funcionario() { }

        public Funcionario(string nomeFuncionario, string cpf, int filialId)
        {
            Id = Guid.NewGuid();
            NomeFuncionario = nomeFuncionario.Trim();
            Cpf = cpf.Trim();
            FilialId = filialId;
        }

        public void Update(string nomeFuncionario, string cpf, int filialId)
        {
            NomeFuncionario = nomeFuncionario.Trim();
            Cpf = cpf.Trim();
            FilialId = filialId;
        }
    }
}