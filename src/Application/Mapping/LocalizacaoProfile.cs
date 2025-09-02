using AutoMapper;
using EasyMoto.src.Application.DTOs.Request;
using EasyMoto.src.Application.DTOs.Response;
using EasyMoto.src.Domain.Entities;

namespace EasyMoto.src.Application.Mapping
{
    public class LocalizacaoProfile : Profile
    {
        public LocalizacaoProfile()
        {
            CreateMap<Localizacao, LocalizacaoResponseDto>();
            CreateMap<LocalizacaoRequestDto, Localizacao>();
        }
    }
}
