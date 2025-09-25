using EasyMoto.Application.Operadores.Contracts;
using EasyMoto.Domain.Entities;
using EasyMoto.Domain.Repositories;

namespace EasyMoto.Application.Operadores
{
    public class CriarOperadorHandler
    {
        private readonly IOperadorRepository _repo;

        public CriarOperadorHandler(IOperadorRepository repo)
        {
            _repo = repo;
        }

        public async Task<OperadorResponse> ExecuteAsync(CriarOperadorRequest req, CancellationToken ct)
        {
            var entity = new Operador(req.NomeOperador, req.Cpf, req.Telefone, req.Email, req.FilialId);
            await _repo.AddAsync(entity, ct);
            return new OperadorResponse(entity.Id, entity.NomeOperador, entity.Cpf, entity.Telefone, entity.Email, entity.FilialId);
        }
    }
}