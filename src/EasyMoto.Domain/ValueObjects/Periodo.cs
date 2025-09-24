using System;
using System.Collections.Generic;
using EasyMoto.Domain.Abstractions;

namespace EasyMoto.Domain.ValueObjects
{
    public sealed class Periodo : ValueObject
    {
        public DateTime Inicio { get; private set; }
        public DateTime Fim { get; private set; }

        private Periodo() { }

        public Periodo(DateTime inicio, DateTime fim)
        {
            if (fim <= inicio) throw new ArgumentException("Periodo inválido");
            Inicio = inicio;
            Fim = fim;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Inicio;
            yield return Fim;
        }
    }
}