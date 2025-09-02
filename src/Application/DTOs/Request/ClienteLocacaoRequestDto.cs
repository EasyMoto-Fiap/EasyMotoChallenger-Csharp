namespace EasyMoto.src.Application.DTOs.Request
{
    public class ClienteLocacaoRequestDto
    {
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
        public string StatusLocacao { get; set; }
        public int ClienteId { get; set; }
    }
}
