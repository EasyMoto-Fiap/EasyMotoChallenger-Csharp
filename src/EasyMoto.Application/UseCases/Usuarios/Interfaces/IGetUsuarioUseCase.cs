using EasyMoto.Application.DTOs.Usuarios;

namespace EasyMoto.Application.UseCases.Usuarios.Interfaces;

public interface IGetUsuarioUseCase
{
    Task<UsuarioResponse?> Execute(int id);
}