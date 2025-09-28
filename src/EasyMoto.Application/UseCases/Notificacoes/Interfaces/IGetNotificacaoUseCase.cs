using EasyMoto.Application.DTOs.Notificacoes;

namespace EasyMoto.Application.UseCases.Notificacoes.Interfaces
{
    public interface IGetNotificacaoUseCase
    {
        Task<NotificacaoResponse?> Execute(int id);
    }
}