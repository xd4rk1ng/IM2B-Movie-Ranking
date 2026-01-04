namespace IM2B.ViewModels.Filme
{
    public class AtorViewModel
    {
        public int Id { get; set; }
        public string Nome { get; set; } = "";
        public string Personagem { get; set; } = "";
        public bool Principal { get; set; }
        public DateOnly DataNasc { get; set; }
        public DateOnly? DataObito { get; set; }
    }
}