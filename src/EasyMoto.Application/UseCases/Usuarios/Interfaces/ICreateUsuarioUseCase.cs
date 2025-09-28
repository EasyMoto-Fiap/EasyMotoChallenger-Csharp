using EasyMoto.Application.DTOs.Usuarios;

namespace EasyMoto.Application.UseCases.Usuarios.Interfaces;

public interface ICreateUsuarioUseCase
{
    Task<UsuarioResponse> Execute(CreateUsuarioRequest request);
}