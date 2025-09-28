using Microsoft.EntityFrameworkCore;
using EasyMoto.Domain.Entities;
using EasyMoto.Domain.Repositories;

namespace EasyMoto.Infrastructure.Persistence.Repositories
{
    public class FilialRepository : RepositoryBase<Filial>, IFilialRepository
    {
        private readonly AppDbContext _db;

        public FilialRepository(AppDbContext context) : base(context)
        {
            _db = context;
        }

        async Task IRepository<Filial>.AddAsync(Filial entity)
        {
            await base.AddAsync(entity);
        }

        async Task<int> IRepository<Filial>.CountAsync()
        {
            return await _db.Set<Filial>().AsNoTracking().CountAsync();
        }

        async Task<IReadOnlyList<Filial>> IRepository<Filial>.ListAsync(int page, int pageSize)
        {
            var query = _db.Set<Filial>().AsNoTracking().OrderBy(x => x.Id);
            var items = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
            return items;
        }
    }
}