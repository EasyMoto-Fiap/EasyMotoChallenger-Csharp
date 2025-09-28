using EasyMoto.Application.DTOs.Filiais;
using EasyMoto.Application.Mapping;
using EasyMoto.Domain.Entities;
using EasyMoto.Domain.Repositories;

namespace EasyMoto.Application.UseCases.Filiais
{
    public interface ICreateFilialUseCase
    {
        Task<FilialResponse> Execute(CreateFilialRequest request);
    }

    public class CreateFilialUseCase : ICreateFilialUseCase
    {
        private readonly IFilialRepository _repo;

        public CreateFilialUseCase(IFilialRepository repo)
        {
            _repo = repo;
        }

        public async Task<FilialResponse> Execute(CreateFilialRequest request)
        {
            var entity = new Filial(request.Nome, request.Cep, request.Cidade, request.Uf);
            await _repo.AddAsync(entity);
            return FilialMapper.ToResponse(entity);
        }
    }
}