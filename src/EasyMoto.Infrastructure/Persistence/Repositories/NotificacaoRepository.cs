using Microsoft.EntityFrameworkCore;
using EasyMoto.Domain.Entities;
using EasyMoto.Domain.Repositories;

namespace EasyMoto.Infrastructure.Persistence.Repositories
{
    public class NotificacaoRepository : RepositoryBase<Notificacao>, INotificacaoRepository
    {
        private readonly AppDbContext _db;

        public NotificacaoRepository(AppDbContext context) : base(context)
        {
            _db = context;
        }

        async Task IRepository<Notificacao>.AddAsync(Notificacao entity)
        {
            await base.AddAsync(entity);
        }

        async Task<int> IRepository<Notificacao>.CountAsync()
        {
            return await _db.Set<Notificacao>().AsNoTracking().CountAsync();
        }

        async Task<IReadOnlyList<Notificacao>> IRepository<Notificacao>.ListAsync(int page, int pageSize)
        {
            var q = _db.Set<Notificacao>().AsNoTracking().Include(n => n.Leituras).OrderByDescending(x => x.CriadaEm);
            return await q.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
        }

        async Task<Notificacao?> IRepository<Notificacao>.GetByIdAsync(int id)
        {
            return await _db.Set<Notificacao>().Include(n => n.Leituras).FirstOrDefaultAsync(n => n.Id == id);
        }

        public async Task MarcarComoLidaAsync(int notificacaoId, int usuarioId)
        {
            var exists = await _db.Set<NotificacaoLeitura>().AsNoTracking().AnyAsync(x => x.NotificacaoId == notificacaoId && x.UsuarioId == usuarioId);
            if (!exists)
            {
                _db.Set<NotificacaoLeitura>().Add(new NotificacaoLeitura { NotificacaoId = notificacaoId, UsuarioId = usuarioId, LidoEm = DateTime.UtcNow });
                await _db.SaveChangesAsync();
            }
        }
    }
}