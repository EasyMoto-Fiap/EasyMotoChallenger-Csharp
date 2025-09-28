using EasyMoto.Application.DTOs.Motos;

namespace EasyMoto.Application.UseCases.Motos.Interfaces
{
    public interface IUpdateMotoUseCase
    {
        Task<MotoResponse?> Execute(int id, UpdateMotoRequest request);
    }
}