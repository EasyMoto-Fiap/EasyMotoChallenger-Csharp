using EasyMoto.Application.DTOs.Motos;
using EasyMoto.Application.UseCases.Motos.Implementations;
using EasyMoto.Tests.TestDoubles;

namespace EasyMoto.Tests.Unit.Motos
{
    public class GetMotoUseCaseTests
    {
        [Fact]
        public async Task DeveRetornarMoto_QuandoExiste()
        {
            var repo = new InMemoryMotoRepository();
            var create = new CreateMotoUseCase(repo);

            await create.Execute(new CreateMotoRequest
            {
                Modelo = "XRE 300",
                Ano = 2024,
                Placa = "ABC1D23",
                Cor = "Preta",
                Ativo = true,
                FilialId = 1
            });

            var get = new GetMotoUseCase(repo);
            var criada = repo.Query().Single();
            var result = await get.Execute(criada.Id);

            var moto = Assert.IsType<MotoResponse>(result);
            Assert.Equal(criada.Id, moto.Id);
            Assert.Equal("XRE 300", moto.Modelo);
            Assert.Equal("ABC1D23", moto.Placa);
        }
    }
}