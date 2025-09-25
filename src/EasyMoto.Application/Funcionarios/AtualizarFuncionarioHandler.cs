using System.Threading;
using System.Threading.Tasks;
using EasyMoto.Application.Funcionarios.Contracts;
using EasyMoto.Domain.Repositories;

namespace EasyMoto.Application.Funcionarios
{
    public sealed class AtualizarFuncionarioHandler
    {
        private readonly IFuncionarioRepository _repo;
        public AtualizarFuncionarioHandler(IFuncionarioRepository repo) => _repo = repo;

        public async Task<FuncionarioResponse?> ExecuteAsync(System.Guid id, AtualizarFuncionarioRequest req, CancellationToken ct = default)
        {
            var entity = await _repo.GetByIdAsync(id, ct);
            if (entity is null) return null;

            entity.Update(req.NomeFuncionario, req.Cpf, req.FilialId);
            await _repo.UpdateAsync(entity, ct);

            return new FuncionarioResponse
            {
                IdFuncionario = entity.Id,
                NomeFuncionario = entity.NomeFuncionario,
                Cpf = entity.Cpf,
                FilialId = entity.FilialId
            };
        }
    }
}