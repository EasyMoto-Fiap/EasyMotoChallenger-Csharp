using EasyMoto.Application.DTOs.Motos;
using EasyMoto.Application.Mapping;
using EasyMoto.Application.UseCases.Motos.Interfaces;
using EasyMoto.Domain.Repositories;

namespace EasyMoto.Application.UseCases.Motos.Implementations
{
    public class UpdateMotoUseCase : IUpdateMotoUseCase
    {
        private readonly IMotoRepository _repo;

        public UpdateMotoUseCase(IMotoRepository repo)
        {
            _repo = repo;
        }

        public async Task<MotoResponse?> Execute(int id, UpdateMotoRequest request)
        {
            var entity = await _repo.GetByIdAsync(id);
            if (entity == null) return null;

            entity.Placa = request.Placa;
            entity.Modelo = request.Modelo;
            entity.Ano = request.Ano;
            entity.Cor = request.Cor;
            entity.Ativo = request.Ativo;
            entity.FilialId = request.FilialId;
            entity.Categoria = (Domain.Entities.CategoriaMoto)request.Categoria;
            entity.StatusOperacional = (Domain.Entities.StatusOperacionalMoto)request.StatusOperacional;
            entity.LegendaStatusId = request.LegendaStatusId;
            entity.QrCode = request.QrCode;
            entity.UpdatedAt = DateTime.UtcNow;

            await _repo.UpdateAsync(entity);
            return MotoMapper.ToResponse(entity);
        }
    }
}