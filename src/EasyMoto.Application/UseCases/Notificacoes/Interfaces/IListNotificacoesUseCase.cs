using EasyMoto.Application.DTOs.Common;
using EasyMoto.Application.DTOs.Notificacoes;

namespace EasyMoto.Application.UseCases.Notificacoes.Interfaces
{
    public interface IListNotificacoesUseCase
    {
        Task<PagedResponse<NotificacaoResponse>> Execute(int page, int pageSize);
    }
}