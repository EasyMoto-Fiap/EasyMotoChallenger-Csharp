namespace EasyMoto.Application.UseCases.Motos.Interfaces
{
    public interface IDeleteMotoUseCase
    {
        Task<bool> Execute(int id);
    }
}