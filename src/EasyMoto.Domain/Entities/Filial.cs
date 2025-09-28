namespace EasyMoto.Domain.Entities
{
    public class Filial
    {
        public int Id { get; private set; }
        public string Nome { get; private set; }
        public string Cep { get; private set; }
        public string Cidade { get; private set; }
        public string Uf { get; private set; }

        public Filial(string nome, string cep, string cidade, string uf)
        {
            Nome = nome;
            Cep = cep;
            Cidade = cidade;
            Uf = uf;
        }

        public void Atualizar(string nome, string cep, string cidade, string uf)
        {
            Nome = nome;
            Cep = cep;
            Cidade = cidade;
            Uf = uf;
        }
    }
}