using EasyMoto.Domain.Entities;

namespace EasyMoto.Domain.Repositories
{
    public interface INotificacaoRepository : IRepository<Notificacao>
    {
        Task MarcarComoLidaAsync(int notificacaoId, int usuarioId);
    }
}