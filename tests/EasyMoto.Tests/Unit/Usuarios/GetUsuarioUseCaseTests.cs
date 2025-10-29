using EasyMoto.Application.DTOs.Usuarios;
using EasyMoto.Application.UseCases.Usuarios.Implementations;
using EasyMoto.Tests.TestDoubles;

namespace EasyMoto.Tests.Unit.Usuarios
{
    public class GetUsuarioUseCaseTests
    {
        [Fact]
        public async Task DeveRetornarUsuario_QuandoExiste()
        {
            var repo = new InMemoryUsuarioRepository();
            var create = new CreateUsuarioUseCase(repo);

            await create.Execute(new CreateUsuarioRequest
            {
                NomeCompleto = "Ana Souza",
                Email = "ana@exemplo.com",
                Telefone = "11911112222",
                Cpf = "98765432100",
                CepFilial = "01001-000",
                Senha = "senha123",
                ConfirmarSenha = "senha123",
                FilialId = 1,
                Ativo = true
            });

            var get = new GetUsuarioUseCase(repo);
            var criado = repo.Query().Single();
            var result = await get.Execute(criado.Id);

            var usuario = Assert.IsType<UsuarioResponse>(result);
            Assert.Equal(criado.Id, usuario.Id);
            Assert.Equal("Ana Souza", usuario.NomeCompleto);
            Assert.Equal("ana@exemplo.com", usuario.Email);
            Assert.True(usuario.Ativo);
        }

        [Fact]
        public async Task DeveRetornarNull_QuandoNaoExiste()
        {
            var repo = new InMemoryUsuarioRepository();
            var get = new GetUsuarioUseCase(repo);

            var result = await get.Execute(999);

            Assert.Null(result);
        }
    }
}