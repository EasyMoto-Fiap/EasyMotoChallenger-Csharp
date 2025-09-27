namespace EasyMoto.Domain.Entities;

public sealed class Moto
{
    public int Id { get; private set; }
    public string Placa { get; private set; } = string.Empty;
    public string Marca { get; private set; } = string.Empty;
    public string Modelo { get; private set; } = string.Empty;
    public string Cor { get; private set; } = string.Empty;
    public int AnoFabricacao { get; private set; }
    public string Status { get; private set; } = "Disponivel";

    public int? LocacaoId { get; private set; }
    public int LocalizacaoId { get; private set; }

    public Moto(
        string placa,
        string marca,
        string modelo,
        string cor,
        int anoFabricacao,
        string status,
        int? locacaoId,
        int localizacaoId)
    {
        Update(placa, marca, modelo, cor, anoFabricacao, status, locacaoId, localizacaoId);
    }

    public void Update(
        string placa,
        string marca,
        string modelo,
        string cor,
        int anoFabricacao,
        string status,
        int? locacaoId,
        int localizacaoId)
    {
        Placa = placa.Trim();
        Marca = marca.Trim();
        Modelo = modelo.Trim();
        Cor = cor.Trim();
        AnoFabricacao = anoFabricacao;
        Status = string.IsNullOrWhiteSpace(status) ? "Disponivel" : status.Trim();
        LocacaoId = locacaoId;
        LocalizacaoId = localizacaoId;
    }
}