using System;
using System.Collections.Generic;
using System.Linq;
using EasyMoto.Domain.Abstractions;

namespace EasyMoto.Domain.ValueObjects;

public sealed class Telefone : ValueObject
{
    public string Value { get; }

    public Telefone(string value)
    {
        if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException("Telefone inválido");

        var digits = new string(value.Where(char.IsDigit).ToArray());
        if (digits.Length < 10 || digits.Length > 11) throw new ArgumentException("Telefone inválido");

        Value = digits;
    }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }

    public static implicit operator string(Telefone t) => t.Value;
    public static implicit operator Telefone(string value) => new(value);
}