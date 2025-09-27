using EasyMoto.Application.Clientes.Contracts;
using EasyMoto.Domain.Repositories;

namespace EasyMoto.Application.Clientes
{
    public sealed class AtualizarClienteHandler
    {
        private readonly IClienteRepository _repo;
        public AtualizarClienteHandler(IClienteRepository repo) => _repo = repo;

        public async Task<bool> ExecuteAsync(int id, AtualizarClienteRequest req, CancellationToken ct = default)
        {
            var entity = await _repo.GetByIdAsync(id, ct);
            if (entity is null) return false;

            var duplicated = await _repo.ExistsCpfAsync(req.Cpf, id, ct);
            if (duplicated) throw new InvalidOperationException("Já existe cliente com este CPF.");

            entity.Update(req.Nome, req.Cpf, req.Telefone, req.Email);
            await _repo.UpdateAsync(entity, ct);
            return true;
        }
    }
}