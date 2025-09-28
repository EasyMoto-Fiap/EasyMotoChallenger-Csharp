using EasyMoto.Application.DTOs.Common;
using EasyMoto.Application.DTOs.Motos;

namespace EasyMoto.Application.UseCases.Motos.Interfaces
{
    public interface IListMotosUseCase
    {
        Task<PagedResponse<MotoResponse>> Execute(int page, int pageSize);
    }
}