namespace EasyMoto.Domain.Repositories;

public interface IRepositoryBase<T> where T : class
{
    Task<T> AddAsync(T entity);
    Task<T?> GetByIdAsync(int id);
    Task UpdateAsync(T entity);
    Task DeleteAsync(T entity);
    IQueryable<T> Query();
    Task<int> SaveChangesAsync();
}