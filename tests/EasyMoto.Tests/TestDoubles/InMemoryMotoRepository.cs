using System.Reflection;
using EasyMoto.Domain.Entities;
using EasyMoto.Domain.Repositories;

namespace EasyMoto.Tests.TestDoubles
{
    internal sealed class InMemoryMotoRepository : IMotoRepository
    {
        private readonly List<Moto> _db = new();
        private int _nextId = 1;

        private static readonly PropertyInfo IdProp = typeof(Moto).GetProperty(nameof(Moto.Id))!;

        public Task<Moto?> GetByIdAsync(int id)
        {
            return Task.FromResult(_db.FirstOrDefault(x => x.Id == id));
        }

        public Task<IReadOnlyList<Moto>> ListAsync(int page, int pageSize)
        {
            if (page < 1) page = 1;
            if (pageSize < 1) pageSize = 10;
            var items = _db.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            return Task.FromResult((IReadOnlyList<Moto>)items);
        }

        public Task<int> CountAsync()
        {
            return Task.FromResult(_db.Count);
        }

        public Task AddAsync(Moto entity)
        {
            var set = IdProp.GetSetMethod(true)!;
            set.Invoke(entity, new object[] { _nextId++ });
            _db.Add(entity);
            return Task.CompletedTask;
        }

        public Task UpdateAsync(Moto entity)
        {
            var i = _db.FindIndex(x => x.Id == entity.Id);
            if (i >= 0) _db[i] = entity;
            return Task.CompletedTask;
        }

        public Task DeleteAsync(Moto entity)
        {
            _db.RemoveAll(x => x.Id == entity.Id);
            return Task.CompletedTask;
        }

        public IQueryable<Moto> Query()
        {
            return _db.AsQueryable();
        }
    }
}