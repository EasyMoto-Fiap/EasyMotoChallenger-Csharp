using EasyMoto.Domain.Abstractions;

namespace EasyMoto.Domain.ValueObjects;

public sealed class Cpf : ValueObject
{
    public string Value { get; }

    private Cpf(string value)
    {
        Value = value;
    }

    public static Cpf From(string input)
    {
        if (input is null) throw new ArgumentNullException(nameof(input));
        var digits = new string(input.Where(char.IsDigit).ToArray());
        if (digits.Length != 11) throw new ArgumentException("CPF inválido", nameof(input));
        if (new string(digits[0], 11) == digits) throw new ArgumentException("CPF inválido", nameof(input));
        var nums = digits.Select(c => c - '0').ToArray();
        int CalcDigit(int[] source, int startWeight)
        {
            var sum = 0;
            for (var i = 0; i < source.Length; i++) sum += source[i] * (startWeight - i);
            var mod = sum % 11;
            return mod < 2 ? 0 : 11 - mod;
        }
        var d1 = CalcDigit(nums[..9], 10);
        var ten = nums[..9].Concat(new[] { d1 }).ToArray();
        var d2 = CalcDigit(ten, 11);
        if (d1 != nums[9] || d2 != nums[10]) throw new ArgumentException("CPF inválido", nameof(input));
        return new Cpf(digits);
    }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }

    public override string ToString() => Value;
}