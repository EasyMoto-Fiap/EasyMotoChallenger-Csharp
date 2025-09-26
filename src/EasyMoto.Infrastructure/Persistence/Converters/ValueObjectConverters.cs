using EasyMoto.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EasyMoto.Infrastructure.Persistence.Converters
{
    public static class ValueObjectConverters
    {
        public static readonly ValueConverter<Cpf, string> CpfConverter =
            new ValueConverter<Cpf, string>(v => v.Value, v => Cpf.Create(v));

        public static readonly ValueConverter<Telefone, string> TelefoneConverter =
            new ValueConverter<Telefone, string>(v => v.Value, v => new Telefone(v));

        public static readonly ValueConverter<Email, string> EmailConverter =
            new ValueConverter<Email, string>(v => v.Value, v => new Email(v));
    }
}