using System.Linq;
using EasyMoto.Application.DTOs.Common;
using EasyMoto.Application.DTOs.Usuarios;
using EasyMoto.Application.UseCases.Usuarios.Interfaces;
using EasyMoto.Domain.Repositories;

namespace EasyMoto.Application.UseCases.Usuarios.Implementations
{
    public class ListUsuariosUseCase : IListUsuariosUseCase
    {
        private readonly IUsuarioRepository _repo;

        public ListUsuariosUseCase(IUsuarioRepository repo)
        {
            _repo = repo;
        }

        public async Task<PagedResponse<UsuarioResponse>> Execute(int page, int pageSize)
        {
            var result = await _repo.ListAsync(page, pageSize);

            var items = result.Items.Select(e => new UsuarioResponse
            {
                Id = e.Id,
                NomeCompleto = e.NomeCompleto,
                Email = e.Email,
                Telefone = e.Telefone,
                Cpf = e.Cpf,
                CepFilial = e.CepFilial,
                Perfil = (int)e.Perfil,
                Ativo = e.Ativo,
                FilialId = e.FilialId
            }).ToList();

            return new PagedResponse<UsuarioResponse>(items, result.TotalCount, result.Page, result.PageSize);
        }
    }
}