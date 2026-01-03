namespace context.Entities
{
    public class FilmeEntity
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Sinopse { get; set; }
        public DateOnly DataLancamento { get; set; }
        public TimeSpan Duracao { get; set; }
        public List<AtorEntity> Atores { get; set; } = new();
        public int Avaliacao { get; set; }
    }
}
