using System.ComponentModel.DataAnnotations;

namespace EasyMoto.Application.Motos.Contracts;

public sealed class CriarMotoRequest
{
    [Required, StringLength(10, MinimumLength = 3)]
    public string Placa { get; init; } = string.Empty;
}