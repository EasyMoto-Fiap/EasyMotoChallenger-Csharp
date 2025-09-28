using EasyMoto.Application.DTOs.Common;
using EasyMoto.Application.DTOs.Notificacoes;
using EasyMoto.Application.Mapping;
using EasyMoto.Application.UseCases.Notificacoes.Interfaces;
using EasyMoto.Domain.Repositories;

namespace EasyMoto.Application.UseCases.Notificacoes.Implementations
{
    public class ListNotificacoesUseCase : IListNotificacoesUseCase
    {
        private readonly INotificacaoRepository _repo;

        public ListNotificacoesUseCase(INotificacaoRepository repo)
        {
            _repo = repo;
        }

        public async Task<PagedResponse<NotificacaoResponse>> Execute(int page, int pageSize)
        {
            var total = await _repo.CountAsync();
            var items = await _repo.ListAsync(page, pageSize);
            var data = items.Select(NotificacaoMapper.ToResponse).ToList();
            return new PagedResponse<NotificacaoResponse>(data, page, pageSize, total);
        }
    }
}