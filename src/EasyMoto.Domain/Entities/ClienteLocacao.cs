using System;
using EasyMoto.Domain.ValueObjects;

namespace EasyMoto.Domain.Entities
{
    public sealed class ClienteLocacao
    {
        public Guid Id { get; private set; }
        public Guid ClienteId { get; private set; }
        public Guid MotoId { get; private set; }
        public Periodo Periodo { get; private set; } = default!;
        public string StatusLocacao { get; private set; } = "Ativa";

        private ClienteLocacao() { }

        public ClienteLocacao(Guid clienteId, Guid motoId, Periodo periodo, string statusLocacao)
        {
            Id = Guid.NewGuid();
            Update(clienteId, motoId, periodo, statusLocacao);
        }

        public void Update(Guid clienteId, Guid motoId, Periodo periodo, string statusLocacao)
        {
            ClienteId = clienteId;
            MotoId = motoId;
            Periodo = periodo ?? throw new ArgumentNullException(nameof(periodo));
            StatusLocacao = string.IsNullOrWhiteSpace(statusLocacao) ? "Ativa" : statusLocacao.Trim();
        }
    }
}