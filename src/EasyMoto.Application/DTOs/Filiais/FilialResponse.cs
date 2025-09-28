namespace EasyMoto.Application.DTOs.Filiais
{
    public class FilialResponse
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Cep { get; set; } = string.Empty;
        public string Cidade { get; set; } = string.Empty;
        public string Uf { get; set; } = string.Empty;
        public Dictionary<string, string> Links { get; set; } = new();
    }
}