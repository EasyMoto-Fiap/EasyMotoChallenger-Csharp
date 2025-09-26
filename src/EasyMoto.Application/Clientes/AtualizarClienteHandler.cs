using EasyMoto.Application.Clientes.Contracts;
using EasyMoto.Domain.Repositories;
using EasyMoto.Domain.ValueObjects;

namespace EasyMoto.Application.Clientes
{
    public class AtualizarClienteHandler
    {
        private readonly IClienteRepository _repo;

        public AtualizarClienteHandler(IClienteRepository repo)
        {
            _repo = repo;
        }

        public async Task ExecuteAsync(Guid id, AtualizarClienteRequest req, CancellationToken ct)
        {
            var entity = await _repo.GetByIdAsync(id, ct);
            if (entity is null) return;

            var cpf = Cpf.Create(req.Cpf);
            var telefone = new Telefone(req.Telefone);
            var email = new Email(req.Email);

            entity.Update(req.Nome, cpf, telefone, email);
            await _repo.UpdateAsync(entity, ct);
        }
    }
}