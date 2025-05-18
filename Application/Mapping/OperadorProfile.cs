using AutoMapper;
using EasyMoto.Domain.Entities;
using EasyMoto.Application.DTOs.Request;
using EasyMoto.Application.DTOs.Response;

namespace EasyMoto.Application.Mapping
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
