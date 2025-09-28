using EasyMoto.Application.DTOs.Motos;

namespace EasyMoto.Application.UseCases.Motos.Interfaces
{
    public interface IGetMotoUseCase
    {
        Task<MotoResponse?> Execute(int id);
    }
}