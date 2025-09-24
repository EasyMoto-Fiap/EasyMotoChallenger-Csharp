using System;
using System.Threading;
using System.Threading.Tasks;
using EasyMoto.Domain.Repositories;

namespace EasyMoto.Application.ClienteLocacoes
{
    public sealed class ExcluirClienteLocacaoHandler
    {
        private readonly IClienteLocacaoRepository _repo;
        public ExcluirClienteLocacaoHandler(IClienteLocacaoRepository repo) => _repo = repo;

        public async Task ExecuteAsync(Guid id, CancellationToken ct = default)
        {
            var e = await _repo.GetByIdAsync(id, ct);
            if (e is null) return;
            await _repo.DeleteAsync(e, ct);
        }
    }
}