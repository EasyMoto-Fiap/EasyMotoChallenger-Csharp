using EasyMoto.Application.Operadores.Contracts;
using EasyMoto.Domain.Repositories;

namespace EasyMoto.Application.Operadores
{
    public class ObterOperadorPorIdHandler
    {
        private readonly IOperadorRepository _repo;

        public ObterOperadorPorIdHandler(IOperadorRepository repo)
        {
            _repo = repo;
        }

        public async Task<OperadorResponse?> ExecuteAsync(Guid id, CancellationToken ct)
        {
            var entity = await _repo.GetByIdAsync(id, ct);
            if (entity == null) return null;
            return new OperadorResponse(entity.Id, entity.NomeOperador, entity.Cpf, entity.Telefone, entity.Email, entity.FilialId);
        }
    }
}