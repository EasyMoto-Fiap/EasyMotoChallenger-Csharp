using EasyMoto.Application.UseCases.Motos.Interfaces;
using EasyMoto.Domain.Repositories;

namespace EasyMoto.Application.UseCases.Motos.Implementations
{
    public class DeleteMotoUseCase : IDeleteMotoUseCase
    {
        private readonly IMotoRepository _repo;

        public DeleteMotoUseCase(IMotoRepository repo)
        {
            _repo = repo;
        }

        public async Task<bool> Execute(int id)
        {
            var entity = await _repo.GetByIdAsync(id);
            if (entity == null) return false;
            await _repo.DeleteAsync(entity);
            return true;
        }
    }
}