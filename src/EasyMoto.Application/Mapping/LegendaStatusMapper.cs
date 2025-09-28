using EasyMoto.Application.DTOs.Legendas;
using EasyMoto.Domain.Entities;

namespace EasyMoto.Application.Mapping
{
    public static class LegendaStatusMapper
    {
        public static LegendaStatusResponse ToResponse(LegendaStatus e)
        {
            return new LegendaStatusResponse
            {
                Id = e.Id,
                Titulo = e.Titulo,
                Descricao = e.Descricao,
                CorHex = e.CorHex,
                Ativo = e.Ativo,
                CreatedAt = e.CreatedAt,
                UpdatedAt = e.UpdatedAt
            };
        }
    }
}