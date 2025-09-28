using EasyMoto.Application.DTOs.Notificacoes;
using EasyMoto.Domain.Entities;

namespace EasyMoto.Application.Mapping
{
    public static class NotificacaoMapper
    {
        public static NotificacaoResponse ToResponse(Notificacao e)
        {
            return new NotificacaoResponse
            {
                Id = e.Id,
                Tipo = (int)e.Tipo,
                Mensagem = e.Mensagem,
                MotoId = e.MotoId,
                UsuarioOrigemId = e.UsuarioOrigemId,
                CriadaEm = e.CriadaEm,
                Escopo = (int)e.Escopo,
                LidaPor = e.Leituras.Select(x => x.UsuarioId).ToList()
            };
        }
    }
}