using EasyMoto.Application.DTOs.Legendas;
using EasyMoto.Application.Mapping;
using EasyMoto.Application.UseCases.Legendas.Interfaces;
using EasyMoto.Domain.Repositories;

namespace EasyMoto.Application.UseCases.Legendas.Implementations
{
    public class UpdateLegendaStatusUseCase : IUpdateLegendaStatusUseCase
    {
        private readonly ILegendaStatusRepository _repo;

        public UpdateLegendaStatusUseCase(ILegendaStatusRepository repo)
        {
            _repo = repo;
        }

        public async Task<LegendaStatusResponse?> Execute(int id, UpdateLegendaStatusRequest request)
        {
            var e = await _repo.GetByIdAsync(id);
            if (e == null) return null;

            e.Titulo = request.Titulo;
            e.Descricao = request.Descricao;
            e.CorHex = request.CorHex;
            e.Ativo = request.Ativo;
            e.UpdatedAt = DateTime.UtcNow;

            await _repo.UpdateAsync(e);
            return LegendaStatusMapper.ToResponse(e);
        }
    }
}