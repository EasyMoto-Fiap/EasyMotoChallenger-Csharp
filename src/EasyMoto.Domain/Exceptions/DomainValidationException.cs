namespace EasyMoto.Domain.Exceptions;

public sealed class DomainValidationException : Exception
{
    public string? Field { get; }
    public DomainValidationException(string message, string? field = null) : base(message) => Field = field;
}