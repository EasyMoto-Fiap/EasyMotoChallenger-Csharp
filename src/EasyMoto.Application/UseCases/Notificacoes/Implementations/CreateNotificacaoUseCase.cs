using EasyMoto.Application.DTOs.Notificacoes;
using EasyMoto.Application.Mapping;
using EasyMoto.Application.UseCases.Notificacoes.Interfaces;
using EasyMoto.Domain.Entities;
using EasyMoto.Domain.Repositories;

namespace EasyMoto.Application.UseCases.Notificacoes.Implementations
{
    public class CreateNotificacaoUseCase : ICreateNotificacaoUseCase
    {
        private readonly INotificacaoRepository _repo;

        public CreateNotificacaoUseCase(INotificacaoRepository repo)
        {
            _repo = repo;
        }

        public async Task<NotificacaoResponse> Execute(CreateNotificacaoRequest request)
        {
            var e = new Notificacao
            {
                Tipo = (TipoNotificacao)request.Tipo,
                Mensagem = request.Mensagem,
                MotoId = request.MotoId,
                UsuarioOrigemId = request.UsuarioOrigemId,
                CriadaEm = DateTime.UtcNow,
                Escopo = (EscopoNotificacao)request.Escopo
            };
            await _repo.AddAsync(e);
            return NotificacaoMapper.ToResponse(e);
        }
    }
}