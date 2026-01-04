namespace shared.Models
{
    public class Ator
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public DateOnly DataNasc { get; set; }
        public DateOnly? DataObito { get; set; }
        public string Biografia { get; set; }
        public List<Filme> Filmes { get; set; } = new();

        // Construtor Vazio (NAO REMOVER)
        public Ator() { }

        // Placeholder Constructor
        public Ator(
            int id,
            string nome,
            DateOnly dataNasc,
            DateOnly? dataObito,
            string biografia,
            List<Filme> filmes
            )
        {
            Id = id;
            Nome = nome;
            DataNasc = dataNasc;
            DataObito = dataObito;
            Biografia = biografia;
            Filmes = filmes;
        }
    }
}
