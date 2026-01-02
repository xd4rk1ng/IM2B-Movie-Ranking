namespace context.Entities
{
    public class AtorEntity
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public DateOnly DataNasc { get; set; }
        public DateOnly? DataObito { get; set; }
        public string Biografia { get; set; }
        public List<FilmeEntity> Filmes { get; set; }
    }
}
