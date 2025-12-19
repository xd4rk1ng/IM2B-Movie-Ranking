namespace IM2B.Models
{
    public class Ator
    {
        public string Nome { get; set; }
        public DateOnly DataNasc { get; set; }
        public DateOnly? DataObito { get; set; }
        public string Biografia { get; set; }
        public List<Filme> Filmes { get; set; }
    }
}
