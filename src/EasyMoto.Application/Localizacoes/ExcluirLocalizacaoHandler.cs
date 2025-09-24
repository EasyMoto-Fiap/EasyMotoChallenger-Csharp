using System;
using System.Threading;
using System.Threading.Tasks;
using EasyMoto.Domain.Repositories;

namespace EasyMoto.Application.Localizacoes
{
    public sealed class ExcluirLocalizacaoHandler
    {
        private readonly ILocalizacaoRepository _repo;

        public ExcluirLocalizacaoHandler(ILocalizacaoRepository repo)
        {
            _repo = repo;
        }

        public async Task<bool> ExecuteAsync(Guid id, CancellationToken ct)
        {
            var entity = await _repo.GetByIdAsync(id, ct);
            if (entity == null) return false;
            await _repo.DeleteAsync(entity, ct);
            return true;
        }
    }
}