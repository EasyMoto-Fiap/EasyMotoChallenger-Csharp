namespace EasyMoto.Application.Operadores.Contracts
{
    public record OperadorResponse(int Id, string NomeOperador, string Cpf, string Telefone, string Email, int FilialId);
}