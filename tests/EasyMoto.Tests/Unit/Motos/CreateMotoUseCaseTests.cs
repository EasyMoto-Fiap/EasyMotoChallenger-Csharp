using EasyMoto.Application.DTOs.Motos;
using EasyMoto.Application.UseCases.Motos.Implementations;
using EasyMoto.Tests.TestDoubles;

namespace EasyMoto.Tests.Unit.Motos
{
    public class CreateMotoUseCaseTests
    {
        [Fact]
        public async Task DeveCriarMoto_ComDadosCorretos()
        {
            var repo = new InMemoryMotoRepository();
            var useCase = new CreateMotoUseCase(repo);

            var request = new CreateMotoRequest
            {
                Modelo = "CG 160",
                Ano = 2022,
                Placa = "ABC1234",
                Cor = "Preta",
                Ativo = true,
                FilialId = 10
            };

            await useCase.Execute(request);

            var criada = repo.Query().Single();
            Assert.Equal("ABC1234", criada.Placa);
            Assert.Equal("CG 160", criada.Modelo);
            Assert.Equal(2022, criada.Ano);
            Assert.Equal("Preta", criada.Cor);
            Assert.True(criada.Ativo);
            Assert.Equal(10, criada.FilialId);
            Assert.True(criada.Id > 0);
        }
    }
}