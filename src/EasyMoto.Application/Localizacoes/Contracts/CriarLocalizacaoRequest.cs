using System;

namespace EasyMoto.Application.Localizacoes.Contracts
{
    public sealed class CriarLocalizacaoRequest
    {
        public string? StatusLoc { get; set; }
        public DateTime? DataHora { get; set; }
        public string? ZonaVirtual { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
    }
}