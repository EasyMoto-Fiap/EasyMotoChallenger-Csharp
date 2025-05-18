using AutoMapper;
using EasyMoto.Domain.Entities;
using EasyMoto.Application.DTOs.Request;
using EasyMoto.Application.DTOs.Response;

namespace EasyMoto.Application.Mapping
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
