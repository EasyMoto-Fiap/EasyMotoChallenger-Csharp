using EasyMoto.Application.Filiais.Contracts;
using EasyMoto.Domain.Entities;
using EasyMoto.Domain.Repositories;

namespace EasyMoto.Application.Filiais
{
    public sealed class CriarFilialHandler
    {
        private readonly IFilialRepository _repo;
        public CriarFilialHandler(IFilialRepository repo) => _repo = repo;

        public async Task<FilialResponse> ExecuteAsync(CriarFilialRequest req, CancellationToken ct = default)
        {
            var entity = new Filial(req.NomeFilial, req.Cidade, req.Estado, req.Pais, req.Endereco, req.EmpresaId);
            entity = await _repo.AddAsync(entity, ct);
            return new FilialResponse
            {
                IdFilial = entity.IdFilial,
                NomeFilial = entity.NomeFilial,
                Cidade = entity.Cidade,
                Estado = entity.Estado,
                Pais = entity.Pais,
                Endereco = entity.Endereco,
                EmpresaId = entity.EmpresaId
            };
        }
    }
}