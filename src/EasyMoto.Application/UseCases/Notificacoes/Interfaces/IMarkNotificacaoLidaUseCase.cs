namespace EasyMoto.Application.UseCases.Notificacoes.Interfaces
{
    public interface IMarkNotificacaoLidaUseCase
    {
        Task<bool> Execute(int notificacaoId, int usuarioId);
    }
}