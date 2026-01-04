using System.ComponentModel.DataAnnotations;

namespace IM2B.ViewModels.Ator
{
    public class FormAtorViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome do ator é obrigatório.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "A data de nascimento é obrigatória.")]
        [DataType(DataType.Date)]
        [Display(Name = "Data de Nascimento")]
        public DateOnly DataNasc { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Data de Óbito")]
        public DateOnly? DataObito { get; set; }

        [Required(ErrorMessage = "A biografia é obrigatória.")]
        [DataType(DataType.MultilineText)]
        public string Biografia { get; set; }

        // For displaying related movies; optional in the view
        //public List<FilmeListItemViewModel> Filmes { get; set; }
    }
}
