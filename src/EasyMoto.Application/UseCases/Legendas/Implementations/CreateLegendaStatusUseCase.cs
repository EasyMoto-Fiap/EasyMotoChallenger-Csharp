using EasyMoto.Application.DTOs.Legendas;
using EasyMoto.Application.Mapping;
using EasyMoto.Application.UseCases.Legendas.Interfaces;
using EasyMoto.Domain.Entities;
using EasyMoto.Domain.Repositories;

namespace EasyMoto.Application.UseCases.Legendas.Implementations
{
    public class CreateLegendaStatusUseCase : ICreateLegendaStatusUseCase
    {
        private readonly ILegendaStatusRepository _repo;

        public CreateLegendaStatusUseCase(ILegendaStatusRepository repo)
        {
            _repo = repo;
        }

        public async Task<LegendaStatusResponse> Execute(CreateLegendaStatusRequest request)
        {
            var e = new LegendaStatus
            {
                Titulo = request.Titulo,
                Descricao = request.Descricao,
                CorHex = request.CorHex,
                Ativo = request.Ativo,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };
            await _repo.AddAsync(e);
            return LegendaStatusMapper.ToResponse(e);
        }
    }
}