using System.Text.RegularExpressions;
using EasyMoto.Domain.Abstractions;
using EasyMoto.Domain.Exceptions;

namespace EasyMoto.Domain.ValueObjects;

public sealed class Telefone : ValueObject
{
    public string Digitos { get; }
    public string Value => Digitos;

    public Telefone(string value)
    {
        var digits = Regex.Replace(value ?? string.Empty, @"\D", "");
        if (digits.Length is not (10 or 11)) throw new DomainValidationException("Telefone deve conter DDD (10 ou 11 dígitos).", "telefone");
        if (digits[0] == '0' || digits[1] == '0') throw new DomainValidationException("DDD inválido.", "telefone");
        Digitos = digits;
    }

    public override string ToString() =>
        Digitos.Length == 11
            ? $"({Digitos[..2]}) {Digitos[2]}{Digitos[3..7]}-{Digitos[7..]}"
            : $"({Digitos[..2]}) {Digitos[2..6]}-{Digitos[6..]}";

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Digitos;
    }
}