namespace EasyMoto.src.Application.DTOs.Response
{
    public class ClienteLocacaoResponseDto
    {
        public int IdLocacao { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
        public string StatusLocacao { get; set; }
        public int ClienteId { get; set; }
    }
}
