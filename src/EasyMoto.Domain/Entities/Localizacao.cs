namespace EasyMoto.Domain.Entities;

public class Localizacao
{
    public int Id { get; private set; }
    public string? StatusLoc { get; private set; }
    public DateTime? DataHora { get; private set; }
    public string? ZonaVirtual { get; private set; }
    public double? Latitude { get; private set; }
    public double? Longitude { get; private set; }

    public Localizacao() { }

    public Localizacao(string? statusLoc, DateTime? dataHora, string? zonaVirtual, double? latitude, double? longitude)
    {
        StatusLoc = statusLoc;
        DataHora = dataHora;
        ZonaVirtual = zonaVirtual;
        Latitude = latitude;
        Longitude = longitude;
    }

    public void Atualizar(string? statusLoc, DateTime? dataHora, string? zonaVirtual, double? latitude, double? longitude)
    {
        StatusLoc = statusLoc;
        DataHora = dataHora;
        ZonaVirtual = zonaVirtual;
        Latitude = latitude;
        Longitude = longitude;
    }
}