using EasyMoto.Application.DTOs.Filiais;
using EasyMoto.Domain.Entities;

namespace EasyMoto.Application.Mapping
{
    public static class FilialMapper
    {
        public static FilialResponse ToResponse(Filial entity)
        {
            return new FilialResponse
            {
                Id = entity.Id,
                Nome = entity.Nome,
                Cep = entity.Cep,
                Cidade = entity.Cidade,
                Uf = entity.Uf
            };
        }
    }
}