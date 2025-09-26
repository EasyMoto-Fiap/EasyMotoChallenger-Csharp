namespace EasyMoto.Application.Operadores.Contracts
{
    public record AtualizarOperadorRequest(int Id, string NomeOperador, string Cpf, string Telefone, string Email, int FilialId);
}