using EasyMoto.Domain.Repositories;

namespace EasyMoto.Application.UseCases.Filiais
{
    public interface IDeleteFilialUseCase
    {
        Task<bool> Execute(int id);
    }

    public class DeleteFilialUseCase : IDeleteFilialUseCase
    {
        private readonly IFilialRepository _repo;

        public DeleteFilialUseCase(IFilialRepository repo)
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