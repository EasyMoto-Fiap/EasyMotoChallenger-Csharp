using EasyMoto.Application.DTOs.Common;
using EasyMoto.Application.DTOs.Motos;
using EasyMoto.Application.Mapping;
using EasyMoto.Application.UseCases.Motos.Interfaces;
using EasyMoto.Domain.Repositories;

namespace EasyMoto.Application.UseCases.Motos.Implementations
{
    public class ListMotosUseCase : IListMotosUseCase
    {
        private readonly IMotoRepository _repo;

        public ListMotosUseCase(IMotoRepository repo)
        {
            _repo = repo;
        }

        public async Task<PagedResponse<MotoResponse>> Execute(int page, int pageSize)
        {
            var total = await _repo.CountAsync();
            var items = await _repo.ListAsync(page, pageSize);
            var data = items.Select(MotoMapper.ToResponse).ToList();
            return new PagedResponse<MotoResponse>(data, page, pageSize, total);
        }
    }
}