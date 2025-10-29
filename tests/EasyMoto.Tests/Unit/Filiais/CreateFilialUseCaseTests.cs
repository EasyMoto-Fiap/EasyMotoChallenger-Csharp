using EasyMoto.Application.DTOs.Filiais;
using EasyMoto.Application.UseCases.Filiais;
using EasyMoto.Tests.TestDoubles;

namespace EasyMoto.Tests.Unit.Filiais
{
    public class CreateFilialUseCaseTests
    {
        [Fact]
        public async Task DeveCriarFilial_ComDadosCorretos()
        {
            var repo = new InMemoryFilialRepository();
            var useCase = new CreateFilialUseCase(repo);

            var request = new CreateFilialRequest
            {
                Nome = "Filial Centro",
                Cep = "01001-000",
                Cidade = "São Paulo",
                Uf = "SP"
            };

            await useCase.Execute(request);

            var criada = repo.Query().Single();
            Assert.Equal("Filial Centro", criada.Nome);
            Assert.True(criada.Id > 0);
        }
    }
}