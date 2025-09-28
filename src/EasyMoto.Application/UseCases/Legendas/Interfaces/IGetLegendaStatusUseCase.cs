using EasyMoto.Application.DTOs.Legendas;

namespace EasyMoto.Application.UseCases.Legendas.Interfaces
{
    public interface IGetLegendaStatusUseCase
    {
        Task<LegendaStatusResponse?> Execute(int id);
    }
}