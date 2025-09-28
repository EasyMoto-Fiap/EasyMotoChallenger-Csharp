using EasyMoto.Domain.Entities;

namespace EasyMoto.Domain.Repositories
{
    public interface IUsuarioRepository : IRepository<Usuario>
    {
        Task<bool> EmailExisteAsync(string email);
        Task<Usuario?> ObterPorEmailAsync(string email);

        new Task<(IList<Usuario> Items, int TotalCount, int Page, int PageSize)> ListAsync(int page, int pageSize);
    }
}