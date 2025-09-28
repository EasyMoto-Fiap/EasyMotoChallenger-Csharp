using EasyMoto.Application.DTOs.Legendas;

namespace EasyMoto.Application.UseCases.Legendas.Interfaces
{
    public interface IUpdateLegendaStatusUseCase
    {
        Task<LegendaStatusResponse?> Execute(int id, UpdateLegendaStatusRequest request);
    }
}