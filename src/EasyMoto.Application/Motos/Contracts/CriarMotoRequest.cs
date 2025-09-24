using System;

namespace EasyMoto.Application.Motos.Contracts
{
    public sealed class CriarMotoRequest
    {
        public string Placa { get; init; } = null!;
        public string Modelo { get; init; } = null!;
        public int AnoFabricacao { get; init; }
        public string Status { get; init; } = null!;
        public Guid? LocacaoId { get; init; }
        public Guid? LocalizacaoId { get; init; }
    }
}