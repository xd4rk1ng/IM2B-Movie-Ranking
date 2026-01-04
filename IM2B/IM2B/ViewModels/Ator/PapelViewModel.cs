using shared.Models;

namespace IM2B.ViewModels.Ator
{
    public class PapelViewModel
    {
        public int Id { get; set; }
        public int FilmeId { get; set; }
        public string FilmeTitulo { get; set; }
        public string Personagem { get; set; }
        public bool Principal { get; set; }
        //public Filme Filme { get; set; }
        public DateOnly DataLancamento { get; set; }
    }
}
