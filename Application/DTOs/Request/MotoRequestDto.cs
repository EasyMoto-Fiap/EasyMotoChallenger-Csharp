namespace EasyMoto.Application.DTOs.Request
{
    public class MotoRequestDto
    {
        public string Placa { get; set; }
        public string Modelo { get; set; }
        public int AnoFabricacao { get; set; }
        public string StatusMoto { get; set; }
        public int LocacaoId { get; set; }
        public int LocalizacaoId { get; set; }
    }
}
