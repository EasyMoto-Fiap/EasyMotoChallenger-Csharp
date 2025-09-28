using EasyMoto.Application.DTOs.Common;
using EasyMoto.Application.DTOs.Legendas;

namespace EasyMoto.Application.UseCases.Legendas.Interfaces
{
    public interface IListLegendasStatusUseCase
    {
        Task<PagedResponse<LegendaStatusResponse>> Execute(int page, int pageSize);
    }
}