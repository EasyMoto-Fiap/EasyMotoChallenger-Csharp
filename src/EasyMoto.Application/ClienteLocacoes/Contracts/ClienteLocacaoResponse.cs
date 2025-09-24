using System;

namespace EasyMoto.Application.ClienteLocacoes.Contracts
{
    public sealed class ClienteLocacaoResponse
    {
        public Guid Id { get; init; }
        public Guid ClienteId { get; init; }
        public Guid MotoId { get; init; }
        public DateTime DataInicio { get; init; }
        public DateTime DataFim { get; init; }
        public string StatusLocacao { get; init; } = "";
    }
}