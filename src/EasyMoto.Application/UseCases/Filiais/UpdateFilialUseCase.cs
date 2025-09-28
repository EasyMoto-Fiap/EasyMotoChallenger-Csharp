using EasyMoto.Application.DTOs.Filiais;
using EasyMoto.Application.Mapping;
using EasyMoto.Domain.Repositories;

namespace EasyMoto.Application.UseCases.Filiais
{
    public interface IUpdateFilialUseCase
    {
        Task<bool> Execute(int id, UpdateFilialRequest request);
    }

    public class UpdateFilialUseCase : IUpdateFilialUseCase
    {
        private readonly IFilialRepository _repo;

        public UpdateFilialUseCase(IFilialRepository repo)
        {
            _repo = repo;
        }

        public async Task<bool> Execute(int id, UpdateFilialRequest request)
        {
            var entity = await _repo.GetByIdAsync(id);
            if (entity == null) return false;
            entity.Atualizar(request.Nome, request.Cep, request.Cidade, request.Uf);
            await _repo.UpdateAsync(entity);
            return true;
        }
    }
}