using AutoMapper;
using EasyMoto.Domain.Entities;
using EasyMoto.Application.DTOs.Request;
using EasyMoto.Application.DTOs.Response;

namespace EasyMoto.Application.Mapping
{
    public class FilialProfile : Profile
    {
        public FilialProfile()
        {
            CreateMap<Filial, FilialResponseDto>();
            CreateMap<FilialRequestDto, Filial>();
        }
    }
}
