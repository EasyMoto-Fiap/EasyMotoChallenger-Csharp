using EasyMoto.Application.Filiais.Contracts;
using EasyMoto.Domain.Repositories;

namespace EasyMoto.Application.Filiais
{
    public sealed class ObterFilialPorIdHandler
    {
        private readonly IFilialRepository _repo;
        public ObterFilialPorIdHandler(IFilialRepository repo) => _repo = repo;

        public async Task<FilialResponse?> ExecuteAsync(int id, CancellationToken ct = default)
        {
            var e = await _repo.GetByIdAsync(id, ct);
            if (e == null) return null;
            return new FilialResponse
            {
                IdFilial = e.IdFilial,
                NomeFilial = e.NomeFilial,
                Cidade = e.Cidade,
                Estado = e.Estado,
                Pais = e.Pais,
                Endereco = e.Endereco,
                EmpresaId = e.EmpresaId
            };
        }
    }
}