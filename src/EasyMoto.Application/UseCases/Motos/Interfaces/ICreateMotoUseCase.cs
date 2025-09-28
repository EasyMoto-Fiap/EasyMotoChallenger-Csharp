using EasyMoto.Application.DTOs.Motos;

namespace EasyMoto.Application.UseCases.Motos.Interfaces
{
    public interface ICreateMotoUseCase
    {
        Task<MotoResponse> Execute(CreateMotoRequest request);
    }
}