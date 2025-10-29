using EasyMoto.Application.DTOs.Filiais;
using EasyMoto.Application.UseCases.Filiais;
using EasyMoto.Domain.Entities;
using EasyMoto.Tests.TestDoubles;

namespace EasyMoto.Tests.Unit.Filiais
{
    public class GetFilialUseCaseTests
    {
        [Fact]
        public async Task DeveRetornarFilial_QuandoExiste()
        {
            var seed = new[]
            {
                new Filial("Filial Norte", "99999-999", "Manaus", "AM")
            };

            var repo = new InMemoryFilialRepository(seed);
            var useCase = new GetFilialUseCase(repo);

            var result = await useCase.Execute(1);

            var filial = Assert.IsType<FilialResponse>(result);
            Assert.Equal("Filial Norte", filial.Nome);
        }

        [Fact]
        public async Task DeveRetornarNull_QuandoNaoExiste()
        {
            var repo = new InMemoryFilialRepository();
            var useCase = new GetFilialUseCase(repo);

            var result = await useCase.Execute(99);

            Assert.Null(result);
        }
    }
}