using EasyMoto.Domain.Abstractions;

namespace EasyMoto.Domain.ValueObjects;

public sealed class Cpf : ValueObject
{
    public string Value { get; }

    private Cpf(string value) => Value = value;

    public static Cpf From(string input)
    {
        var digits = new string((input ?? string.Empty).Where(char.IsDigit).ToArray());
        if (digits.Length != 11) throw new ArgumentException("CPF inválido");
        if (digits.Distinct().Count() == 1) throw new ArgumentException("CPF inválido");

        var nums = digits.Select(c => c - '0').ToArray();

        var sum = 0;
        for (var i = 0; i < 9; i++) sum += nums[i] * (10 - i);
        var d1 = sum % 11; d1 = d1 < 2 ? 0 : 11 - d1;
        if (nums[9] != d1) throw new ArgumentException("CPF inválido");

        sum = 0;
        for (var i = 0; i < 10; i++) sum += nums[i] * (11 - i);
        var d2 = sum % 11; d2 = d2 < 2 ? 0 : 11 - d2;
        if (nums[10] != d2) throw new ArgumentException("CPF inválido");

        return new Cpf(digits);
    }

    public override string ToString() => Value;

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }
}
