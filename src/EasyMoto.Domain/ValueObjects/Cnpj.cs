namespace EasyMoto.Domain.ValueObjects;

public sealed class Cnpj : IEquatable<Cnpj>
{
    public string Value { get; }
    private Cnpj(string value) { Value = value; }

    public static Cnpj Create(string input)
    {
        var digits = OnlyDigits(input);
        if (!IsValidInternal(digits)) throw new ArgumentException("CNPJ inválido");
        return new Cnpj(digits);
    }

    public static bool TryCreate(string input, out Cnpj? cnpj)
    {
        try
        {
            cnpj = Create(input);
            return true;
        }
        catch
        {
            cnpj = null;
            return false;
        }
    }

    public override string ToString() => Value;

    public bool Equals(Cnpj? other) => other is not null && Value == other.Value;
    public override bool Equals(object? obj) => obj is Cnpj other && Equals(other);
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
        if (digits.Length != 14) return false;
        var weights1 = new[] {5,4,3,2,9,8,7,6,5,4,3,2};
        var weights2 = new[] {6,5,4,3,2,9,8,7,6,5,4,3,2};

        int Calc(string s, int[] w)
        {
            var sum = 0;
            for (var i = 0; i < w.Length; i++) sum += (s[i] - '0') * w[i];
            var r = sum % 11;
            return r < 2 ? 0 : 11 - r;
        }

        var dv1 = Calc(digits, weights1);
        if ((digits[12] - '0') != dv1) return false;
        var dv2 = Calc(digits, weights2);
        return (digits[13] - '0') == dv2;
    }
}