using EasyMoto.Application.DTOs.Notificacoes;

namespace EasyMoto.Application.UseCases.Notificacoes.Interfaces
{
    public interface ICreateNotificacaoUseCase
    {
        Task<NotificacaoResponse> Execute(CreateNotificacaoRequest request);
    }
}