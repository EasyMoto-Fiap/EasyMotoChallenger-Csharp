using AutoMapper;
using EasyMoto.Domain.Entities;
using EasyMoto.Application.DTOs.Request;
using EasyMoto.Application.DTOs.Response;

namespace EasyMoto.Application.Mapping
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
