namespace EasyMoto.Application.UseCases.Usuarios.Interfaces;

public interface IDeleteUsuarioUseCase
{
    Task<bool> Execute(int id);
}