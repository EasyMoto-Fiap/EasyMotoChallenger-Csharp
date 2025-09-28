using EasyMoto.Application.DTOs.Motos;
using EasyMoto.Domain.Entities;

namespace EasyMoto.Application.Mapping
{
    public static class MotoMapper
    {
        public static MotoResponse ToResponse(Moto entity)
        {
            return new MotoResponse
            {
                Id = entity.Id,
                Placa = entity.Placa,
                Modelo = entity.Modelo,
                Ano = entity.Ano,
                Cor = entity.Cor,
                Ativo = entity.Ativo,
                FilialId = entity.FilialId,
                Categoria = (int)entity.Categoria,
                StatusOperacional = (int)entity.StatusOperacional,
                LegendaStatusId = entity.LegendaStatusId,
                QrCode = entity.QrCode,
                CreatedAt = entity.CreatedAt,
                UpdatedAt = entity.UpdatedAt
            };
        }
    }
}