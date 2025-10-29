using System.Reflection;
using EasyMoto.Domain.Entities;
using EasyMoto.Domain.Repositories;

namespace EasyMoto.Tests.TestDoubles
{
    internal sealed class InMemoryUsuarioRepository : IUsuarioRepository
    {
        private readonly List<Usuario> _db = new();
        private int _nextId = 1;

        private static readonly PropertyInfo IdProp =
            typeof(Usuario).GetProperty(nameof(Usuario.Id))
            ?? throw new InvalidOperationException("Property Usuario.Id not found.");

        public InMemoryUsuarioRepository() { }

        public InMemoryUsuarioRepository(IEnumerable<Usuario>? seed)
        {
            if (seed is null) return;
            foreach (var u in seed) AddInternal(u);
        }

        private void AddInternal(Usuario u)
        {
            var set = IdProp.GetSetMethod(true)!;
            set.Invoke(u, new object[] { _nextId++ });
            _db.Add(u);
        }

        public Task<Usuario?> GetByIdAsync(int id)
        {
            return Task.FromResult(_db.FirstOrDefault(x => x.Id == id));
        }

        public Task<(IList<Usuario> Items, int TotalCount, int Page, int PageSize)> ListAsync(int page, int pageSize)
        {
            if (page < 1) page = 1;
            if (pageSize < 1) pageSize = 10;

            var items = _db.OrderBy(x => x.Id)
                           .Skip((page - 1) * pageSize)
                           .Take(pageSize)
                           .ToList();
            var total = _db.Count;

            return Task.FromResult(((IList<Usuario>)items, total, page, pageSize));
        }

        Task<IReadOnlyList<Usuario>> IRepository<Usuario>.ListAsync(int page, int pageSize)
        {
            if (page < 1) page = 1;
            if (pageSize < 1) pageSize = 10;

            var items = _db.OrderBy(x => x.Id)
                           .Skip((page - 1) * pageSize)
                           .Take(pageSize)
                           .ToList()
                           .AsReadOnly();
            return Task.FromResult((IReadOnlyList<Usuario>)items);
        }

        public Task<int> CountAsync()
        {
            return Task.FromResult(_db.Count);
        }

        public Task AddAsync(Usuario entity)
        {
            AddInternal(entity);
            return Task.CompletedTask;
        }

        public Task UpdateAsync(Usuario entity)
        {
            var i = _db.FindIndex(x => x.Id == entity.Id);
            if (i >= 0) _db[i] = entity;
            return Task.CompletedTask;
        }

        public Task DeleteAsync(Usuario entity)
        {
            _db.RemoveAll(x => x.Id == entity.Id);
            return Task.CompletedTask;
        }

        public IQueryable<Usuario> Query()
        {
            return _db.AsQueryable();
        }

        public Task<bool> EmailExisteAsync(string email)
        {
            return Task.FromResult(_db.Any(x => x.Email == email));
        }

        public Task<Usuario?> ObterPorEmailAsync(string email)
        {
            return Task.FromResult(_db.FirstOrDefault(x => x.Email == email));
        }
    }
}
