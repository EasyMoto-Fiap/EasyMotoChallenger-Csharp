using Microsoft.EntityFrameworkCore;
using EasyMoto.Domain.Entities;
using EasyMoto.Domain.Repositories;

namespace EasyMoto.Infrastructure.Persistence.Repositories
{
    public class UsuarioRepository : RepositoryBase<Usuario>, IUsuarioRepository
    {
        private readonly AppDbContext _db;

        public UsuarioRepository(AppDbContext context) : base(context)
        {
            _db = context;
        }

        public async Task<bool> EmailExisteAsync(string email)
        {
            return await _db.Set<Usuario>()
                .AsNoTracking()
                .AnyAsync(x => x.Email == email);
        }

        public async Task<Usuario?> ObterPorEmailAsync(string email)
        {
            return await _db.Set<Usuario>()
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Email == email);
        }

        public async Task<(IList<Usuario> Items, int TotalCount, int Page, int PageSize)> ListAsync(int page, int pageSize)
        {
            var query = _db.Set<Usuario>().AsNoTracking().OrderBy(x => x.Id);
            var total = await query.CountAsync();
            var items = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
            return (items, total, page, pageSize);
        }

        async Task IRepository<Usuario>.AddAsync(Usuario entity)
        {
            await base.AddAsync(entity);
        }

        async Task<int> IRepository<Usuario>.CountAsync()
        {
            return await _db.Set<Usuario>().AsNoTracking().CountAsync();
        }

        async Task<IReadOnlyList<Usuario>> IRepository<Usuario>.ListAsync(int page, int pageSize)
        {
            var query = _db.Set<Usuario>().AsNoTracking().OrderBy(x => x.Id);
            var items = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
            return items;
        }
    }
}
