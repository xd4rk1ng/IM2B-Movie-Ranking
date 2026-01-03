using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
namespace context.Entities
{
    public class PapelEntity
    {
        public int Id { get; set; }
        public int FilmeId { get; set; }
        public FilmeEntity Filme { get; set; }
        public int AtorId { get; set; }
        public AtorEntity Ator { get; set; }
        public string Personagem { get; set; }
        public bool Principal { get; set; }
    }
}
