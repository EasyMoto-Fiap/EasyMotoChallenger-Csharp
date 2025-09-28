using EasyMoto.Application.DTOs.Legendas;

namespace EasyMoto.Application.UseCases.Legendas.Interfaces
{
    public interface ICreateLegendaStatusUseCase
    {
        Task<LegendaStatusResponse> Execute(CreateLegendaStatusRequest request);
    }
}