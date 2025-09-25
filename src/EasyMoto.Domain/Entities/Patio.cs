using System;

namespace EasyMoto.Domain.Entities
{
    public class Patio
    {
        public Guid Id { get; private set; }
        public string? NomePatio { get; private set; }
        public string TamanhoPatio { get; private set; } = string.Empty;
        public string? Andar { get; private set; }
        public Guid FilialId { get; private set; }

        public Patio(Guid id, string? nomePatio, string tamanhoPatio, string? andar, Guid filialId)
        {
            Id = id;
            NomePatio = nomePatio;
            TamanhoPatio = tamanhoPatio ?? string.Empty;
            Andar = andar;
            FilialId = filialId;
        }

        public void Update(string? nomePatio, string tamanhoPatio, string? andar, Guid filialId)
        {
            NomePatio = nomePatio;
            TamanhoPatio = tamanhoPatio ?? string.Empty;
            Andar = andar;
            FilialId = filialId;
        }
    }
}