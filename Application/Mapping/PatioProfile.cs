using AutoMapper;
using EasyMoto.Domain.Entities;
using EasyMoto.Application.DTOs.Request;
using EasyMoto.Application.DTOs.Response;

namespace EasyMoto.Application.Mapping
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
