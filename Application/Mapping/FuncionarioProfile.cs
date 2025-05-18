using AutoMapper;
using EasyMoto.Domain.Entities;
using EasyMoto.Application.DTOs.Request;
using EasyMoto.Application.DTOs.Response;

namespace EasyMoto.Application.Mapping
{
    public class FuncionarioProfile : Profile
    {
        public FuncionarioProfile()
        {
            CreateMap<Funcionario, FuncionarioResponseDto>();
            CreateMap<FuncionarioRequestDto, Funcionario>();
        }
    }
}
