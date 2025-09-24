using EasyMoto.Application.Clientes.Contracts;
using EasyMoto.Domain.Repositories;
using EasyMoto.Domain.ValueObjects;

namespace EasyMoto.Application.Clientes
{
    public sealed class AtualizarClienteHandler
    {
        private readonly IClienteRepository _repo;
        public AtualizarClienteHandler(IClienteRepository repo) => _repo = repo;

        public async Task ExecuteAsync(Guid id, AtualizarClienteRequest req, CancellationToken ct = default)
        {
            var entity = await _repo.GetByIdAsync(id, ct);
            if (entity is null) throw new KeyNotFoundException("Cliente não encontrado.");
            entity.Update(req.Nome, new Cpf(req.Cpf), new Telefone(req.Telefone), new Email(req.Email));
            await _repo.UpdateAsync(entity, ct);
        }
    }
}