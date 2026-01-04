using shared.Models;

namespace IM2B.ViewModels.Ator
{
    public class DetailsAtorViewModel
    {
        public int Id { get; set; }
        public string Nome { get; set; } = "";
        public DateOnly DataNasc { get; set; }
        public DateOnly? DataObito { get; set; }
        public string? Biografia { get; set; }
        //public IEnumerable<Filme>? Filmes { get; set; } // optional, if you want related movies
    }
}
