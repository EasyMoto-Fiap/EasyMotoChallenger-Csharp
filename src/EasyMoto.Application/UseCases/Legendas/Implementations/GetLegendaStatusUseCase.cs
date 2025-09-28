using EasyMoto.Application.DTOs.Legendas;
using EasyMoto.Application.Mapping;
using EasyMoto.Application.UseCases.Legendas.Interfaces;
using EasyMoto.Domain.Repositories;

namespace EasyMoto.Application.UseCases.Legendas.Implementations
{
    public class GetLegendaStatusUseCase : IGetLegendaStatusUseCase
    {
        private readonly ILegendaStatusRepository _repo;

        public GetLegendaStatusUseCase(ILegendaStatusRepository repo)
        {
            _repo = repo;
        }

        public async Task<LegendaStatusResponse?> Execute(int id)
        {
            var e = await _repo.GetByIdAsync(id);
            if (e == null) return null;
            return LegendaStatusMapper.ToResponse(e);
        }
    }
}