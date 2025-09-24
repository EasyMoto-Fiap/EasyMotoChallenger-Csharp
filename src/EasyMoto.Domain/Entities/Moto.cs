using System;

namespace EasyMoto.Domain.Entities
{
    public sealed class Moto
    {
        public Guid Id { get; private set; }
        public string Placa { get; private set; } = null!;
        public string Modelo { get; private set; } = null!;
        public int AnoFabricacao { get; private set; }
        public string Status { get; private set; } = null!;
        public Guid? LocacaoId { get; private set; }
        public Guid? LocalizacaoId { get; private set; }

        private Moto() { }

        public Moto(string placa, string modelo, int anoFabricacao, string status, Guid? locacaoId, Guid? localizacaoId)
        {
            Id = Guid.NewGuid();
            Update(placa, modelo, anoFabricacao, status, locacaoId, localizacaoId);
        }

        public void Update(string placa, string modelo, int anoFabricacao, string status, Guid? locacaoId, Guid? localizacaoId)
        {
            if (string.IsNullOrWhiteSpace(placa)) throw new ArgumentException("Placa obrigatória");
            if (string.IsNullOrWhiteSpace(modelo)) throw new ArgumentException("Modelo obrigatório");
            if (anoFabricacao < 1900) throw new ArgumentException("Ano inválido");
            if (string.IsNullOrWhiteSpace(status)) throw new ArgumentException("Status obrigatório");
            Placa = placa.Trim().ToUpperInvariant();
            Modelo = modelo.Trim();
            AnoFabricacao = anoFabricacao;
            Status = status.Trim();
            LocacaoId = locacaoId;
            LocalizacaoId = localizacaoId;
        }
    }
}