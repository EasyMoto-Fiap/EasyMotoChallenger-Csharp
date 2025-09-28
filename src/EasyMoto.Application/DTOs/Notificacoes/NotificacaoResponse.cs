namespace EasyMoto.Application.DTOs.Notificacoes;

public class NotificacaoResponse
{
    public int Id { get; set; }
    public int Tipo { get; set; }
    public string Mensagem { get; set; } = default!;
    public int? MotoId { get; set; }
    public int UsuarioOrigemId { get; set; }
    public DateTime CriadaEm { get; set; }
    public int Escopo { get; set; }
    public List<int> LidaPor { get; set; } = new();
    public IDictionary<string, string>? Links { get; set; }
}