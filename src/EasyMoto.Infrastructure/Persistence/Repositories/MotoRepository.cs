using Microsoft.EntityFrameworkCore;
using EasyMoto.Domain.Entities;
using EasyMoto.Domain.Repositories;

namespace EasyMoto.Infrastructure.Persistence.Repositories
{
    public class MotoRepository : RepositoryBase<Moto>, IMotoRepository
    {
        private readonly AppDbContext _db;

        public MotoRepository(AppDbContext context) : base(context)
        {
            _db = context;
        }

        async Task IRepository<Moto>.AddAsync(Moto entity)
        {
            await base.AddAsync(entity);
        }

        async Task<int> IRepository<Moto>.CountAsync()
        {
            return await _db.Set<Moto>().AsNoTracking().CountAsync();
        }

        async Task<IReadOnlyList<Moto>> IRepository<Moto>.ListAsync(int page, int pageSize)
        {
            var query = _db.Set<Moto>().AsNoTracking().OrderBy(x => x.Id);
            var items = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
            return items;
        }
    }
}