namespace EasyMoto.Application.Filiais.Contracts
{
    public sealed class AtualizarFilialRequest
    {
        public string NomeFilial { get; set; } = null!;
        public string Cidade { get; set; } = null!;
        public string Estado { get; set; } = null!;
        public string Pais { get; set; } = null!;
        public string Endereco { get; set; } = null!;
        public int EmpresaId { get; set; }
    }
}