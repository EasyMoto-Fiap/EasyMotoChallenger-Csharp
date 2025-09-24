using System;
using System.Collections.Generic;
using System.Net.Mail;
using EasyMoto.Domain.Abstractions;

namespace EasyMoto.Domain.ValueObjects;

public sealed class Email : ValueObject
{
    public string Value { get; }

    public Email(string value)
    {
        if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException("Email inválido");
        try { _ = new MailAddress(value); }
        catch { throw new ArgumentException("Email inválido"); }

        Value = value.Trim().ToLowerInvariant();
    }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }

    public static implicit operator string(Email e) => e.Value;
    public static implicit operator Email(string value) => new(value);
}