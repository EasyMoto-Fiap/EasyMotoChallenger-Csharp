namespace EasyMoto.Application.Vagas.Contracts
{
    public class VagaResponse
    {
        public Guid Id { get; set; }
        public string NumeroVaga { get; set; } = string.Empty;
        public string StatusVaga { get; set; } = string.Empty;
        public Guid? MotoId { get; set; }
        public Guid PatioId { get; set; }
    }
}