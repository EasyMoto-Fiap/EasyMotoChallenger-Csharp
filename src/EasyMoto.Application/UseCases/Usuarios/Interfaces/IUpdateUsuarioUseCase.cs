using EasyMoto.Application.DTOs.Usuarios;

namespace EasyMoto.Application.UseCases.Usuarios.Interfaces;

public interface IUpdateUsuarioUseCase
{
    Task<bool> Execute(int id, UpdateUsuarioRequest request);
}