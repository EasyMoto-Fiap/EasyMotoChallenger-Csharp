using AutoMapper;
using EasyMoto.src.Application.DTOs.Request;
using EasyMoto.src.Application.DTOs.Response;
using EasyMoto.src.Domain.Entities;

namespace EasyMoto.src.Application.Mapping
{
	public class ClienteLocacaoProfile : Profile
	{
		public ClienteLocacaoProfile()
		{
			CreateMap<ClienteLocacao, ClienteLocacaoResponseDto>();
			CreateMap<ClienteLocacaoRequestDto, ClienteLocacao>();
		}
	}
}
