using System.Reflection;
using EasyMoto.Domain.Entities;
using EasyMoto.Domain.Repositories;

namespace EasyMoto.Tests.TestDoubles
{
    internal sealed class InMemoryFilialRepository : IFilialRepository
    {
        private readonly List<Filial> _db = new();
        private int _nextId = 1;

        public InMemoryFilialRepository() { }

        public InMemoryFilialRepository(IEnumerable<Filial>? seed)
        {
            if (seed is null) return;
            foreach (var f in seed) AddInternal(f);
        }

        private static readonly PropertyInfo IdProp =
            typeof(Filial).GetProperty(nameof(Filial.Id))
            ?? throw new InvalidOperationException("Property Filial.Id not found.");

        private void AddInternal(Filial f)
        {
            var set = IdProp.GetSetMethod(true)!;
            set.Invoke(f, new object[] { _nextId++ });
            _db.Add(f);
        }

        public Task<Filial?> GetByIdAsync(int id)
        {
            return Task.FromResult(_db.FirstOrDefault(x => x.Id == id));
        }

        public Task<IReadOnlyList<Filial>> ListAsync(int page, int pageSize)
        {
            if (page <= 0) page = 1;
            if (pageSize <= 0) pageSize = 10;
            var items = _db.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            return Task.FromResult<IReadOnlyList<Filial>>(items);
        }

        public Task<int> CountAsync()
        {
            return Task.FromResult(_db.Count);
        }

        public Task AddAsync(Filial entity)
        {
            AddInternal(entity);
            return Task.CompletedTask;
        }

        public Task UpdateAsync(Filial entity)
        {
            var i = _db.FindIndex(x => x.Id == entity.Id);
            if (i >= 0) _db[i] = entity;
            return Task.CompletedTask;
        }

        public Task DeleteAsync(Filial entity)
        {
            _db.RemoveAll(x => x.Id == entity.Id);
            return Task.CompletedTask;
        }

        public IQueryable<Filial> Query()
        {
            return _db.AsQueryable();
        }
    }
}
