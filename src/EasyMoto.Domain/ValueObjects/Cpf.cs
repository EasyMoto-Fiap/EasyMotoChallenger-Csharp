using System;
using System.Collections.Generic;
using System.Linq;
using EasyMoto.Domain.Abstractions;

namespace EasyMoto.Domain.ValueObjects;

public sealed class Cpf : ValueObject
{
    public string Value { get; }

    public Cpf(string value)
    {
        if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException("CPF inválido");

        var digits = new string(value.Where(char.IsDigit).ToArray());
        if (digits.Length != 11) throw new ArgumentException("CPF inválido");
        if (!IsValid(digits)) throw new ArgumentException("CPF inválido");

        Value = Convert.ToUInt64(digits).ToString(@"000\.000\.000\-00");
    }

    static bool IsValid(string d)
    {
        if (new string(d[0], 11) == d) return false;

        int Sum(int len, int wstart)
        {
            var s = 0;
            for (int i = 0; i < len; i++) s += (d[i] - '0') * (wstart - i);
            return s;
        }

        var dv1 = (Sum(9, 10) * 10) % 11; if (dv1 == 10) dv1 = 0;
        var dv2 = (Sum(10, 11) * 10) % 11; if (dv2 == 10) dv2 = 0;

        return (d[9] - '0') == dv1 && (d[10] - '0') == dv2;
    }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }

    public static implicit operator string(Cpf c) => c.Value;
    public static implicit operator Cpf(string value) => new(value);
}