using System.ComponentModel.DataAnnotations;

namespace EasyMoto.Application.Clientes.Contracts;

public sealed class CriarClienteRequest
{
    [Required, StringLength(120, MinimumLength = 2)]
    public string Nome { get; init; } = string.Empty;

    [Required, StringLength(14)]
    public string Cpf { get; init; } = string.Empty;
}