using EasyMoto.Application.UseCases.Legendas.Interfaces;
using EasyMoto.Domain.Repositories;

namespace EasyMoto.Application.UseCases.Legendas.Implementations
{
    public class DeleteLegendaStatusUseCase : IDeleteLegendaStatusUseCase
    {
        private readonly ILegendaStatusRepository _repo;

        public DeleteLegendaStatusUseCase(ILegendaStatusRepository repo)
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