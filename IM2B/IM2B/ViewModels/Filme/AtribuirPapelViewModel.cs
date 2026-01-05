using System.ComponentModel.DataAnnotations;

namespace IM2B.ViewModels.Filme
{
    public class AtribuirPapelViewModel
    {
        [Required]
        public int FilmeId { get; set; }
        [Required]
        public int AtorId { get; set; }
        [Required]
        [StringLength(100)]
        public string Personagem { get; set; }
        [Required]
        public bool Principal { get; set; } = false;
    }
}
