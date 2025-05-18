namespace EasyMoto.Application.DTOs.Request
{
    public class LocalizacaoRequestDto
    {
        public string StatusLoc { get; set; }
        public DateTime? DataHora { get; set; }
        public string? ZonaVirtual { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
    }
}
