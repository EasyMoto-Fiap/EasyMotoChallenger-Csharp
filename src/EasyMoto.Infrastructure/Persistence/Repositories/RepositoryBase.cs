using EasyMoto.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace EasyMoto.Infrastructure.Persistence.Repositories;

public class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : class
{
    protected readonly AppDbContext Ctx;
    protected readonly DbSet<TEntity> Set;

    public RepositoryBase(AppDbContext ctx)
    {
        Ctx = ctx;
        Set = ctx.Set<TEntity>();
    }

    public virtual async Task<TEntity> AddAsync(TEntity entity)
    {
        await Set.AddAsync(entity);
        await Ctx.SaveChangesAsync();
        return entity;
    }

    public virtual async Task<TEntity?> GetByIdAsync(int id)
    {
        return await Set.FindAsync(id);
    }

    public virtual async Task UpdateAsync(TEntity entity)
    {
        Set.Update(entity);
        await Ctx.SaveChangesAsync();
    }

    public virtual async Task DeleteAsync(TEntity entity)
    {
        Set.Remove(entity);
        await Ctx.SaveChangesAsync();
    }

    public IQueryable<TEntity> Query()
    {
        return Set.AsQueryable();
    }

    public Task<int> SaveChangesAsync()
    {
        return Ctx.SaveChangesAsync();
    }
}