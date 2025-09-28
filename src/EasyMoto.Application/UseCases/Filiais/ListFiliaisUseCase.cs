using EasyMoto.Application.DTOs.Common;
using EasyMoto.Application.DTOs.Filiais;
using EasyMoto.Application.Mapping;
using EasyMoto.Domain.Repositories;

namespace EasyMoto.Application.UseCases.Filiais
{
    public interface IListFiliaisUseCase
    {
        Task<PagedResponse<FilialResponse>> Execute(int page, int pageSize);
    }

    public class ListFiliaisUseCase : IListFiliaisUseCase
    {
        private readonly IFilialRepository _repo;

        public ListFiliaisUseCase(IFilialRepository repo)
        {
            _repo = repo;
        }

        public async Task<PagedResponse<FilialResponse>> Execute(int page, int pageSize)
        {
            var total = await _repo.CountAsync();
            var items = await _repo.ListAsync(page, pageSize);
            var data = items.Select(FilialMapper.ToResponse).ToList();
            return new PagedResponse<FilialResponse>(data, page, pageSize, total);
        }
    }
}