using System;

namespace EasyMoto.Domain.Entities
{
    public sealed class Filial
    {
        public int IdFilial { get; private set; }
        public string NomeFilial { get; private set; } = null!;
        public string Cidade { get; private set; } = null!;
        public string Estado { get; private set; } = null!;
        public string Pais { get; private set; } = null!;
        public string Endereco { get; private set; } = null!;
        public int EmpresaId { get; private set; }
        public Empresa Empresa { get; private set; } = null!;

        private Filial() { }

        public Filial(string nomeFilial, string cidade, string estado, string pais, string endereco, int empresaId)
        {
            if (string.IsNullOrWhiteSpace(nomeFilial)) throw new ArgumentException(nameof(nomeFilial));
            NomeFilial = nomeFilial.Trim();
            Cidade = cidade?.Trim() ?? "";
            Estado = estado?.Trim() ?? "";
            Pais = pais?.Trim() ?? "";
            Endereco = endereco?.Trim() ?? "";
            EmpresaId = empresaId;
        }

        public void Update(string nomeFilial, string cidade, string estado, string pais, string endereco, int empresaId)
        {
            if (string.IsNullOrWhiteSpace(nomeFilial)) throw new ArgumentException(nameof(nomeFilial));
            NomeFilial = nomeFilial.Trim();
            Cidade = cidade?.Trim() ?? "";
            Estado = estado?.Trim() ?? "";
            Pais = pais?.Trim() ?? "";
            Endereco = endereco?.Trim() ?? "";
            EmpresaId = empresaId;
        }
    }
}