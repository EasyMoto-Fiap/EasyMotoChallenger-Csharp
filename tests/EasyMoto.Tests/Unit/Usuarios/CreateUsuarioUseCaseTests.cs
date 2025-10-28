using EasyMoto.Application.DTOs.Usuarios;
using EasyMoto.Application.UseCases.Usuarios.Implementations;
using EasyMoto.Tests.TestDoubles;

namespace EasyMoto.Tests.Unit.Usuarios
{
    public class CreateUsuarioUseCaseTests
    {
        [Fact]
        public async Task DeveCriarUsuario_ComDadosCorretos()
        {
            var repo = new InMemoryUsuarioRepository();
            var useCase = new CreateUsuarioUseCase(repo);

            var request = new CreateUsuarioRequest
            {
                NomeCompleto = "João Silva",
                Email = "joao@exemplo.com",
                Telefone = "11987654321",
                Cpf = "12345678900",
                CepFilial = "01001-000",
                Senha = "segredo1",
                ConfirmarSenha = "segredo1",
                FilialId = 1,
                Ativo = true
            };

            await useCase.Execute(request);

            var criado = repo.Query().Single();
            Assert.Equal("João Silva", criado.NomeCompleto);
            Assert.Equal("joao@exemplo.com", criado.Email);
            Assert.True(criado.Ativo);
            Assert.True(criado.Id > 0);
        }
    }
}