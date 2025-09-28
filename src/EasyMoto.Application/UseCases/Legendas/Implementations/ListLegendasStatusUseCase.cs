using EasyMoto.Application.DTOs.Common;
using EasyMoto.Application.DTOs.Legendas;
using EasyMoto.Application.Mapping;
using EasyMoto.Application.UseCases.Legendas.Interfaces;
using EasyMoto.Domain.Repositories;

namespace EasyMoto.Application.UseCases.Legendas.Implementations
{
    public class ListLegendasStatusUseCase : IListLegendasStatusUseCase
    {
        private readonly ILegendaStatusRepository _repo;

        public ListLegendasStatusUseCase(ILegendaStatusRepository repo)
        {
            _repo = repo;
        }

        public async Task<PagedResponse<LegendaStatusResponse>> Execute(int page, int pageSize)
        {
            var total = await _repo.CountAsync();
            var itens = await _repo.ListAsync(page, pageSize);
            var data = itens.Select(LegendaStatusMapper.ToResponse).ToList();
            return new PagedResponse<LegendaStatusResponse>(data, page, pageSize, total);
        }
    }
}