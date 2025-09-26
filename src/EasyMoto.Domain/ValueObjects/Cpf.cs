namespace EasyMoto.Domain.ValueObjects;

public sealed class Cpf : IEquatable<Cpf>
{
    public string Value { get; }
    private Cpf(string value) { Value = value; }

    public static Cpf Create(string input)
    {
        var digits = OnlyDigits(input);
        if (!IsValidInternal(digits)) throw new ArgumentException("CPF inválido");
        return new Cpf(digits);
    }

    public static bool TryCreate(string input, out Cpf? cpf)
    {
        try
        {
            cpf = Create(input);
            return true;
        }
        catch
        {
            cpf = null;
            return false;
        }
    }

    public override string ToString() => Value;

    public bool Equals(Cpf? other) => other is not null && Value == other.Value;
    public override bool Equals(object? obj) => obj is Cpf other && Equals(other);
    public override int GetHashCode() => Value.GetHashCode();

    static string OnlyDigits(string input)
    {
        if (string.IsNullOrWhiteSpace(input)) return string.Empty;
        var buffer = new char[input.Length];
        var n = 0;
        foreach (var ch in input)
            if (char.IsDigit(ch)) buffer[n++] = ch;
        return new string(buffer, 0, n);
    }

    static bool IsValidInternal(string digits)
    {
        if (digits.Length != 11) return false;
        var allEqual = true;
        for (var i = 1; i < 11 && allEqual; i++) if (digits[i] != digits[0]) allEqual = false;
        if (allEqual) return false;

        var sum = 0;
        for (var i = 0; i < 9; i++) sum += (digits[i] - '0') * (10 - i);
        var r = sum % 11;
        var dv1 = r < 2 ? 0 : 11 - r;
        if ((digits[9] - '0') != dv1) return false;

        sum = 0;
        for (var i = 0; i < 10; i++) sum += (digits[i] - '0') * (11 - i);
        r = sum % 11;
        var dv2 = r < 2 ? 0 : 11 - r;
        return (digits[10] - '0') == dv2;
    }
}