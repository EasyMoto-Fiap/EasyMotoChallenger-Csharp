using EasyMoto.Domain.Entities;

namespace EasyMoto.Application.Motos.Contracts;

public sealed class MotoResponse
{
    public Guid Id { get; init; }
    public string Placa { get; init; } = string.Empty;
    public MotoStatus Status { get; init; }
}