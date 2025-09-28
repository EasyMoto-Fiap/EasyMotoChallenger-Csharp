using EasyMoto.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace EasyMoto.Infrastructure.Persistence.Repositories
{
    public class RepositoryBase<T> : IRepository<T> where T : class
    {
        private readonly DbContext _context;
        private readonly DbSet<T> _set;

        public RepositoryBase(DbContext context)
        {
            _context = context;
            _set = _context.Set<T>();
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            return await _set.FindAsync(id);
        }

        public async Task<IReadOnlyList<T>> ListAsync(int page, int pageSize)
        {
            return await _set.AsNoTracking().Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
        }

        public async Task<int> CountAsync()
        {
            return await _set.CountAsync();
        }

        public async Task AddAsync(T entity)
        {
            await _set.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            _set.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            _set.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public IQueryable<T> Query()
        {
            return _set.AsQueryable();
        }
    }
}