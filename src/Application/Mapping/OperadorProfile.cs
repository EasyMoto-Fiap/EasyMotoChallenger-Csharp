using AutoMapper;
using EasyMoto.src.Application.DTOs.Request;
using EasyMoto.src.Application.DTOs.Response;
using EasyMoto.src.Domain.Entities;

namespace EasyMoto.src.Application.Mapping
{
    public class OperadorProfile : Profile
    {
        public OperadorProfile()
        {
            CreateMap<Operador, OperadorResponseDto>();
            CreateMap<OperadorRequestDto, Operador>();
        }
    }
}
