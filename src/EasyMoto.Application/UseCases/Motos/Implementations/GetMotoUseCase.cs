using EasyMoto.Application.DTOs.Motos;
using EasyMoto.Application.Mapping;
using EasyMoto.Application.UseCases.Motos.Interfaces;
using EasyMoto.Domain.Repositories;

namespace EasyMoto.Application.UseCases.Motos.Implementations
{
    public class GetMotoUseCase : IGetMotoUseCase
    {
        private readonly IMotoRepository _repo;

        public GetMotoUseCase(IMotoRepository repo)
        {
            _repo = repo;
        }

        public async Task<MotoResponse?> Execute(int id)
        {
            var entity = await _repo.GetByIdAsync(id);
            if (entity == null) return null;
            return EasyMoto.Application.Mapping.MotoMapper.ToResponse(entity);
        }
    }
}