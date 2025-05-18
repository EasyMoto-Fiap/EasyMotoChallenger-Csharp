using AutoMapper;
using EasyMoto.Domain.Entities;
using EasyMoto.Application.DTOs.Request;
using EasyMoto.Application.DTOs.Response;

namespace EasyMoto.Application.Mapping
{
    public class VagaProfile : Profile
    {
        public VagaProfile()
        {
            CreateMap<Vaga, VagaResponseDto>();
            CreateMap<VagaRequestDto, Vaga>();
        }
    }
}
