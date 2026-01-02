namespace shared.Models
{
    public class Filme
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Sinopse { get; set; }
        public DateOnly DataLancamento { get; set; }
        public TimeSpan Duracao { get; set; }
        public List<Ator> Atores { get; set; }
        public int Avaliacao { get; set; }

        // Placeholder Constructor
        public Filme(
            int id,
            string titulo,
            string sinopse,
            DateOnly dataLancamento,
            TimeSpan duracao,
            List<Ator> atores,
            int avaliacao)
        {
            if (string.IsNullOrWhiteSpace(titulo))
                throw new ArgumentException("Titulo inválido");

            Id = id;
            Titulo = titulo;
            Sinopse = sinopse;
            DataLancamento = dataLancamento;
            Duracao = duracao;
            Atores = atores;
            Avaliacao = avaliacao;
        }
    }
}
