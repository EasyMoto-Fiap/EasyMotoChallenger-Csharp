namespace EasyMoto.Application.Patios.Contracts
{
    public sealed class PatioResponse
    {
        public int Id { get; init; }
        public string NomePatio { get; init; } = string.Empty;
        public string TamanhoPatio { get; init; } = string.Empty;
        public string Andar { get; init; } = string.Empty;
        public int FilialId { get; init; }
    }
}