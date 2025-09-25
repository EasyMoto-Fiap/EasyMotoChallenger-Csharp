namespace EasyMoto.Application.Patios.Contracts
{
    public class AtualizarPatioRequest
    {
        public string? NomePatio { get; set; }
        public string TamanhoPatio { get; set; } = string.Empty;
        public string? Andar { get; set; }
        public Guid FilialId { get; set; }
    }
}