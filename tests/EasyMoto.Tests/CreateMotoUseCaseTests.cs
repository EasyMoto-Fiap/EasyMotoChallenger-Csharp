using System.Collections.Concurrent;
using EasyMoto.Application.UseCases.Motos.Implementations;
using EasyMoto.Application.DTOs.Motos;
using EasyMoto.Domain.Repositories;
using EasyMoto.Domain.Entities;

namespace EasyMoto.Tests
{
    internal sealed class FakeMotoRepository : IMotoRepository
    {
        private readonly ConcurrentDictionary<int, Moto> _store = new();
        private int _id;
        public Task AddAsync(Moto entity)
        {
            var id = Interlocked.Increment(ref _id);
            entity.Id = id;
            _store[id] = entity;
            return Task.CompletedTask;
        }
        public Task<int> CountAsync() => Task.FromResult(_store.Count);
        public Task DeleteAsync(Moto entity)
        {
            _store.TryRemove(entity.Id, out _);
            return Task.CompletedTask;
        }
        public Task<Moto?> GetByIdAsync(int id)
        {
            _store.TryGetValue(id, out var m);
            return Task.FromResult(m);
        }
        public Task<IReadOnlyList<Moto>> ListAsync(int page, int pageSize)
        {
            if (page < 1) page = 1;
            if (pageSize < 1) pageSize = 10;
            var items = _store.Values.OrderBy(x => x.Id).Skip((page - 1) * pageSize).Take(pageSize).ToList();
            return Task.FromResult((IReadOnlyList<Moto>)items);
        }
        public IQueryable<Moto> Query() => _store.Values.AsQueryable();
        public Task UpdateAsync(Moto entity)
        {
            _store[entity.Id] = entity;
            return Task.CompletedTask;
        }
    }

    public class CreateMotoUseCaseTests
    {
        [Fact]
        public async Task Execute_DeveCriarMoto_ComDadosCorretos()
        {
            var repo = new FakeMotoRepository();
            var usecase = new CreateMotoUseCase(repo);
            var req = new CreateMotoRequest
            {
                Placa = "ABC1234",
                Modelo = "CG 160",
                Ano = 2023,
                Cor = "Preta",
                Ativo = true,
                FilialId = 1,
                Categoria = 0,
                StatusOperacional = 0,
                LegendaStatusId = null,
                QrCode = "QRCODE-1"
            };
            var resp = await usecase.Execute(req);
            Assert.NotNull(resp);
            Assert.True(resp.Id > 0);
            Assert.Equal(req.Placa, resp.Placa);
            Assert.Equal(req.Modelo, resp.Modelo);
            Assert.Equal(req.Ano, resp.Ano);
            Assert.Equal(req.Cor, resp.Cor);
            Assert.True(resp.Ativo);
            Assert.Equal(1, await repo.CountAsync());
            var salvo = await repo.GetByIdAsync(resp.Id);
            Assert.NotNull(salvo);
            Assert.Equal(req.Placa, salvo!.Placa);
        }
    }
}
