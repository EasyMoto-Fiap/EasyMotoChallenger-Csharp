namespace EasyMoto.src.Application.DTOs.Response
{
    public class LocalizacaoResponseDto
    {
        public int IdLocalizacao { get; set; }
        public string StatusLoc { get; set; }
        public DateTime? DataHora { get; set; }
        public string? ZonaVirtual { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
    }
}
