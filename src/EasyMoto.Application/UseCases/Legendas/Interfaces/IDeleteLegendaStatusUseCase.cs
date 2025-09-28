namespace EasyMoto.Application.UseCases.Legendas.Interfaces
{
    public interface IDeleteLegendaStatusUseCase
    {
        Task<bool> Execute(int id);
    }
}