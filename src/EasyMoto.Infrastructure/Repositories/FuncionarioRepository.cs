using EasyMoto.Domain.Entities;
using EasyMoto.Domain.Repositories;
using EasyMoto.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace EasyMoto.Infrastructure.Repositories
{
    public sealed class FuncionarioRepository : IFuncionarioRepository
    {
        private readonly AppDbContext _ctx;

        public FuncionarioRepository(AppDbContext ctx) => _ctx = ctx;

        public Task<long> CountAsync(CancellationToken ct) =>
            _ctx.Funcionarios.LongCountAsync(ct);

        public async Task<Funcionario> AddAsync(Funcionario entity, CancellationToken ct)
        {
            _ctx.Funcionarios.Add(entity);
            await _ctx.SaveChangesAsync(ct);
            return entity;
        }

        public async Task<bool> DeleteAsync(Guid id, CancellationToken ct)
        {
            var current = await _ctx.Funcionarios.FirstOrDefaultAsync(x => x.Id == id, ct);
            if (current is null) return false;

            _ctx.Funcionarios.Remove(current);
            await _ctx.SaveChangesAsync(ct);
            return true;
        }

        public Task<Funcionario?> GetByIdAsync(Guid id, CancellationToken ct) =>
            _ctx.Funcionarios.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id, ct);

        public Task<IReadOnlyList<Funcionario>> ListAsync(int page, int size, CancellationToken ct) =>
            _ctx.Funcionarios
                .AsNoTracking()
                .OrderBy(x => x.NomeFuncionario)
                .Skip((page - 1) * size)
                .Take(size)
                .ToListAsync(ct)
                .ContinueWith(t => (IReadOnlyList<Funcionario>)t.Result, ct);

        public async Task<Funcionario?> UpdateAsync(Funcionario entity, CancellationToken ct)
        {
            var exists = await _ctx.Funcionarios.AnyAsync(x => x.Id == entity.Id, ct);
            if (!exists) return null;

            _ctx.Attach(entity);
            _ctx.Entry(entity).State = EntityState.Modified;
            await _ctx.SaveChangesAsync(ct);
            return entity;
        }
    }
}
