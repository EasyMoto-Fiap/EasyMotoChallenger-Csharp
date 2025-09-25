using System.Threading;
using System.Threading.Tasks;
using EasyMoto.Application.Funcionarios.Contracts;
using EasyMoto.Domain.Entities;
using EasyMoto.Domain.Repositories;

namespace EasyMoto.Application.Funcionarios
{
    public sealed class CriarFuncionarioHandler
    {
        private readonly IFuncionarioRepository _repo;
        public CriarFuncionarioHandler(IFuncionarioRepository repo) => _repo = repo;

        public async Task<FuncionarioResponse> ExecuteAsync(CriarFuncionarioRequest req, CancellationToken ct = default)
        {
            var entity = new Funcionario(req.NomeFuncionario, req.Cpf, req.FilialId);
            await _repo.AddAsync(entity, ct);

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