using EasyMoto.Application.Operadores.Contracts;
using EasyMoto.Domain.Repositories;

namespace EasyMoto.Application.Operadores
{
    public class AtualizarOperadorHandler
    {
        private readonly IOperadorRepository _repo;

        public AtualizarOperadorHandler(IOperadorRepository repo)
        {
            _repo = repo;
        }

        public async Task<bool> ExecuteAsync(AtualizarOperadorRequest req, CancellationToken ct)
        {
            var entity = await _repo.GetByIdAsync(req.Id, ct);
            if (entity == null) return false;
            entity.Update(req.NomeOperador, req.Cpf, req.Telefone, req.Email, req.FilialId);
            await _repo.UpdateAsync(entity, ct);
            return true;
        }
    }
}