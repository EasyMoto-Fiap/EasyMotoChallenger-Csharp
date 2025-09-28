using EasyMoto.Application.DTOs.Common;
using EasyMoto.Application.DTOs.Usuarios;

namespace EasyMoto.Application.UseCases.Usuarios.Interfaces
{
    public interface IListUsuariosUseCase
    {
        Task<PagedResponse<UsuarioResponse>> Execute(int page, int pageSize);
    }
}