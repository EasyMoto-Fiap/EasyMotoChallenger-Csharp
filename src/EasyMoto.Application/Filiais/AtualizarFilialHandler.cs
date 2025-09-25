using EasyMoto.Application.Filiais.Contracts;
using EasyMoto.Domain.Entities;
using EasyMoto.Domain.Repositories;

namespace EasyMoto.Application.Filiais
{
    public sealed class AtualizarFilialHandler
    {
        private readonly IFilialRepository _repo;
        public AtualizarFilialHandler(IFilialRepository repo) => _repo = repo;

        public async Task<FilialResponse?> ExecuteAsync(int id, AtualizarFilialRequest req, CancellationToken ct = default)
        {
            var input = new Filial(req.NomeFilial, req.Cidade, req.Estado, req.Pais, req.Endereco, req.EmpresaId);
            var updated = await _repo.UpdateAsync(id, input, ct);
            if (updated == null) return null;
            return new FilialResponse
            {
                IdFilial = updated.IdFilial,
                NomeFilial = updated.NomeFilial,
                Cidade = updated.Cidade,
                Estado = updated.Estado,
                Pais = updated.Pais,
                Endereco = updated.Endereco,
                EmpresaId = updated.EmpresaId
            };
        }
    }
}