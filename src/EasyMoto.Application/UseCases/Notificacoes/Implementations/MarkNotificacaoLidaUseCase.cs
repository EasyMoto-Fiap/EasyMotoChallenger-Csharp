using EasyMoto.Application.UseCases.Notificacoes.Interfaces;
using EasyMoto.Domain.Repositories;

namespace EasyMoto.Application.UseCases.Notificacoes.Implementations
{
    public class MarkNotificacaoLidaUseCase : IMarkNotificacaoLidaUseCase
    {
        private readonly INotificacaoRepository _repo;

        public MarkNotificacaoLidaUseCase(INotificacaoRepository repo)
        {
            _repo = repo;
        }

        public async Task<bool> Execute(int notificacaoId, int usuarioId)
        {
            var e = await _repo.GetByIdAsync(notificacaoId);
            if (e == null) return false;
            await _repo.MarcarComoLidaAsync(notificacaoId, usuarioId);
            return true;
        }
    }
}