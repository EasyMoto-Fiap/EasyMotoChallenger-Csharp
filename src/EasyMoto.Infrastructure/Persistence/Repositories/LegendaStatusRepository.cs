using Microsoft.EntityFrameworkCore;
using EasyMoto.Domain.Entities;
using EasyMoto.Domain.Repositories;

namespace EasyMoto.Infrastructure.Persistence.Repositories
{
    public class LegendaStatusRepository : RepositoryBase<LegendaStatus>, ILegendaStatusRepository
    {
        private readonly AppDbContext _db;

        public LegendaStatusRepository(AppDbContext context) : base(context)
        {
            _db = context;
        }

        async Task IRepository<LegendaStatus>.AddAsync(LegendaStatus entity)
        {
            await base.AddAsync(entity);
        }

        async Task<int> IRepository<LegendaStatus>.CountAsync()
        {
            return await _db.Set<LegendaStatus>().AsNoTracking().CountAsync();
        }

        async Task<IReadOnlyList<LegendaStatus>> IRepository<LegendaStatus>.ListAsync(int page, int pageSize)
        {
            var q = _db.Set<LegendaStatus>().AsNoTracking().OrderBy(x => x.Id);
            return await q.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
        }
    }
}