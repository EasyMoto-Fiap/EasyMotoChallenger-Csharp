namespace EasyMoto.Application.DTOs.Request
{
    public class FilialRequestDto
    {
        public string NomeFilial { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public string Pais { get; set; }
        public string Endereco { get; set; }
        public int EmpresaId { get; set; }
    }
}
