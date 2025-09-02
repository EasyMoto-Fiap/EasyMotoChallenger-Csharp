using AutoMapper;
using EasyMoto.src.Application.DTOs.Request;
using EasyMoto.src.Application.DTOs.Response;
using EasyMoto.src.Domain.Entities;

namespace EasyMoto.src.Application.Mapping
{
    public class PatioProfile : Profile
    {
        public PatioProfile()
        {
            CreateMap<Patio, PatioResponseDto>();
            CreateMap<PatioRequestDto, Patio>();
        }
    }
}
