using System;
using EasyMoto.Domain.ValueObjects;

namespace EasyMoto.Domain.Entities
{
    public sealed class ClienteLocacao
    {
        public Guid Id { get; private set; }
        public Guid ClienteId { get; private set; }
        public Guid MotoId { get; private set; }
        public Periodo Periodo { get; private set; } = null!;
        public string Status { get; private set; } = null!;

        private ClienteLocacao() { }

        public ClienteLocacao(Guid clienteId, Guid motoId, Periodo periodo, string status)
        {
            Id = Guid.NewGuid();
            Update(clienteId, motoId, periodo, status);
        }

        public void Update(Guid clienteId, Guid motoId, Periodo periodo, string status)
        {
            if (clienteId == Guid.Empty) throw new ArgumentException("ClienteId obrigatório");
            if (motoId == Guid.Empty) throw new ArgumentException("MotoId obrigatório");
            if (periodo is null) throw new ArgumentNullException(nameof(periodo));
            if (string.IsNullOrWhiteSpace(status)) throw new ArgumentException("Status obrigatório");
            ClienteId = clienteId;
            MotoId = motoId;
            Periodo = periodo;
            Status = status.Trim();
        }
    }
}