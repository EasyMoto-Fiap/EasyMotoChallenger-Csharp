using EasyMoto.Application.UseCases.Notificacoes.Interfaces;
using EasyMoto.Domain.Repositories;

namespace EasyMoto.Application.UseCases.Notificacoes.Implementations
{
    public class DeleteNotificacaoUseCase : IDeleteNotificacaoUseCase
    {
        private readonly INotificacaoRepository _repo;

        public DeleteNotificacaoUseCase(INotificacaoRepository repo)
        {
            _repo = repo;
        }

        public async Task<bool> Execute(int id)
        {
            var e = await _repo.GetByIdAsync(id);
            if (e == null) return false;
            await _repo.DeleteAsync(e);
            return true;
        }
    }
}