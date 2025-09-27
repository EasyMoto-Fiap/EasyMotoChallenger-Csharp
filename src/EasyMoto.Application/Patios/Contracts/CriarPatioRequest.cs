namespace EasyMoto.Application.Patios.Contracts;

public sealed class CriarPatioRequest
{
    public string NomePatio { get; set; } = string.Empty;
    public string TamanhoPatio { get; set; } = string.Empty;
    public string? Andar { get; set; }
    public int FilialId { get; set; }
}