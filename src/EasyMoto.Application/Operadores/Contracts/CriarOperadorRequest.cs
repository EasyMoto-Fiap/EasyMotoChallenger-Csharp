namespace EasyMoto.Application.Operadores.Contracts
{
    public record CriarOperadorRequest(string NomeOperador, string Cpf, string Telefone, string Email, Guid FilialId);
}