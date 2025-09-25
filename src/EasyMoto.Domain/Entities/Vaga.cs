namespace EasyMoto.Domain.Entities
{
    public class Vaga
    {
        public Guid Id { get; private set; }
        public string NumeroVaga { get; private set; } = string.Empty;
        public string StatusVaga { get; private set; } = string.Empty;
        public Guid? MotoId { get; private set; }
        public Guid PatioId { get; private set; }

        public Vaga(Guid id, string numeroVaga, string statusVaga, Guid? motoId, Guid patioId)
        {
            Id = id;
            NumeroVaga = numeroVaga ?? string.Empty;
            StatusVaga = statusVaga ?? string.Empty;
            MotoId = motoId;
            PatioId = patioId;
        }

        public void Update(string numeroVaga, string statusVaga, Guid? motoId, Guid patioId)
        {
            NumeroVaga = numeroVaga ?? string.Empty;
            StatusVaga = statusVaga ?? string.Empty;
            MotoId = motoId;
            PatioId = patioId;
        }
    }
}