using AutoMapper;
using EasyMoto.src.Application.DTOs.Request;
using EasyMoto.src.Application.DTOs.Response;
using EasyMoto.src.Domain.Entities;

namespace EasyMoto.src.Application.Mapping
{
    public class MotoProfile : Profile
    {
        public MotoProfile()
        {
            CreateMap<Moto, MotoResponseDto>();
            CreateMap<MotoRequestDto, Moto>();
        }
    }
}
