namespace EasyMoto.Application.UseCases.Notificacoes.Interfaces
{
    public interface IDeleteNotificacaoUseCase
    {
        Task<bool> Execute(int id);
    }
}