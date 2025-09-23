using System.ComponentModel.DataAnnotations;

namespace EasyMoto.Application.Clientes.Contracts;

public sealed class AtualizarClienteRequest
{
    [Required, StringLength(120, MinimumLength = 2)]
    public string Nome { get; init; } = string.Empty;
}