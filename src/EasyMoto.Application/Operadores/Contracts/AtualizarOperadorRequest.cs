namespace EasyMoto.Application.Operadores.Contracts
{
    public record AtualizarOperadorRequest(Guid Id, string NomeOperador, string Cpf, string Telefone, string Email, Guid FilialId);
}