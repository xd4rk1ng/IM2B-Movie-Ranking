namespace IM2B.Models
{
    public class Papel
    {

        public int Id { get; set; }
        public int FilmeId { get; set; }
        public Filme Filme { get; set; }
        public int AtorId { get; set; }
        public Ator Ator { get; set; }
        public string Personagem { get; set; }
        public bool Principal { get; set; }

        // Placeholder Constructor
        public Papel(
            int id,
            int filmeId,
            Filme filme,
            int atorId,
            Ator ator,
            string personagem,
            bool principal
            )
        {
            Id = id;
            FilmeId = filmeId;
            Filme = filme;
            AtorId = atorId;
            Ator = ator;
            Personagem = personagem;
            Principal = principal;
        }
        
    }
}
