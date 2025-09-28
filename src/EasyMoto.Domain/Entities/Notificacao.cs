namespace EasyMoto.Domain.Entities;

public enum TipoNotificacao
{
    MotoCadastrada = 0,
    MotoAtualizada = 1,
    MotoExcluida = 2
}

public enum EscopoNotificacao
{
    Global = 0,
    Usuario = 1,
    Filial = 2
}

public class Notificacao : EntityBase
{
    public TipoNotificacao Tipo { get; set; }
    public required string Mensagem { get; set; }
    public int? MotoId { get; set; }
    public int UsuarioOrigemId { get; set; }
    public DateTime CriadaEm { get; set; } = DateTime.UtcNow;
    public EscopoNotificacao Escopo { get; set; } = EscopoNotificacao.Global;
    public ICollection<NotificacaoLeitura> Leituras { get; set; } = new List<NotificacaoLeitura>();
}

public class NotificacaoLeitura
{
    public int NotificacaoId { get; set; }
    public int UsuarioId { get; set; }
    public DateTime LidoEm { get; set; } = DateTime.UtcNow;
}