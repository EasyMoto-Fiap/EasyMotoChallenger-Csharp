using EasyMoto.Application.DTOs.Motos;
using EasyMoto.Application.Mapping;
using EasyMoto.Application.UseCases.Motos.Interfaces;
using EasyMoto.Domain.Entities;
using EasyMoto.Domain.Repositories;

namespace EasyMoto.Application.UseCases.Motos.Implementations
{
    public class CreateMotoUseCase : ICreateMotoUseCase
    {
        private readonly IMotoRepository _repo;

        public CreateMotoUseCase(IMotoRepository repo)
        {
            _repo = repo;
        }

        public async Task<MotoResponse> Execute(CreateMotoRequest request)
        {
            var entity = new Moto
            {
                Placa = request.Placa,
                Modelo = request.Modelo,
                Ano = request.Ano,
                Cor = request.Cor,
                Ativo = request.Ativo,
                FilialId = request.FilialId,
                Categoria = (CategoriaMoto)request.Categoria,
                StatusOperacional = (StatusOperacionalMoto)request.StatusOperacional,
                LegendaStatusId = request.LegendaStatusId,
                QrCode = request.QrCode,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            await _repo.AddAsync(entity);
            return MotoMapper.ToResponse(entity);
        }
    }
}