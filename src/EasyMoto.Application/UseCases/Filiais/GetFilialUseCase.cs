using EasyMoto.Application.DTOs.Filiais;
using EasyMoto.Domain.Repositories;

namespace EasyMoto.Application.UseCases.Filiais
{
    public interface IGetFilialUseCase
    {
        Task<FilialResponse?> Execute(int id);
    }

    public class GetFilialUseCase : IGetFilialUseCase
    {
        private readonly IFilialRepository _repo;

        public GetFilialUseCase(IFilialRepository repo)
        {
            _repo = repo;
        }

        public async Task<FilialResponse?> Execute(int id)
        {
            var entity = await _repo.GetByIdAsync(id);
            if (entity == null) return null;
            return Mapping.FilialMapper.ToResponse(entity);
        }
    }
}