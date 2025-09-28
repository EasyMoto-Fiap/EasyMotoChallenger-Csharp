using EasyMoto.Application.DTOs.Notificacoes;
using EasyMoto.Application.Mapping;
using EasyMoto.Application.UseCases.Notificacoes.Interfaces;
using EasyMoto.Domain.Repositories;

namespace EasyMoto.Application.UseCases.Notificacoes.Implementations
{
    public class GetNotificacaoUseCase : IGetNotificacaoUseCase
    {
        private readonly INotificacaoRepository _repo;

        public GetNotificacaoUseCase(INotificacaoRepository repo)
        {
            _repo = repo;
        }

        public async Task<NotificacaoResponse?> Execute(int id)
        {
            var e = await _repo.GetByIdAsync(id);
            if (e == null) return null;
            return NotificacaoMapper.ToResponse(e);
        }
    }
}