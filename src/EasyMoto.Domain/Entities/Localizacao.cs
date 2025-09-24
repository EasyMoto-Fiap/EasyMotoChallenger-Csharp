using System;

namespace EasyMoto.Domain.Entities
{
    public sealed class Localizacao
    {
        public Guid Id { get; private set; }
        public string? StatusLoc { get; private set; }
        public DateTime? DataHora { get; private set; }
        public string? ZonaVirtual { get; private set; }
        public double? Latitude { get; private set; }
        public double? Longitude { get; private set; }

        private Localizacao() { }

        public Localizacao(string? statusLoc, DateTime? dataHora, string? zonaVirtual, double? latitude, double? longitude)
        {
            Id = Guid.NewGuid();
            StatusLoc = statusLoc;
            DataHora = dataHora;
            ZonaVirtual = zonaVirtual;
            Latitude = latitude;
            Longitude = longitude;
        }

        public void Update(string? statusLoc, DateTime? dataHora, string? zonaVirtual, double? latitude, double? longitude)
        {
            StatusLoc = statusLoc;
            DataHora = dataHora;
            ZonaVirtual = zonaVirtual;
            Latitude = latitude;
            Longitude = longitude;
        }
    }
}