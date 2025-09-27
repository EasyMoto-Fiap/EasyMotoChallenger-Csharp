using EasyMoto.Application.Patios.Contracts;
using EasyMoto.Domain.Repositories;

namespace EasyMoto.Application.Patios
{
    public sealed class AtualizarPatioHandler
    {
        private readonly IPatioRepository _repo;

        public AtualizarPatioHandler(IPatioRepository repo) => _repo = repo;

        public async Task<bool> ExecuteAsync(int id, AtualizarPatioRequest req, CancellationToken ct)
        {
            var entity = await _repo.GetByIdAsync(id, ct);
            if (entity is null) return false;
            entity.Update(req.NomePatio, req.TamanhoPatio, req.Andar, req.FilialId);
            await _repo.UpdateAsync(entity, ct);
            return true;
        }
    }
}