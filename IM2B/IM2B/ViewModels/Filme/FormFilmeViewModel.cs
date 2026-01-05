using System;
using System.ComponentModel.DataAnnotations;

namespace IM2B.ViewModels.Filme
{
    public class FormFilmeViewModel
    {
        public int Id { get; set; } // needed for Edit

        [Required(ErrorMessage = "O título do filme é obrigatório.")]
        [Display(Name = "Título")]
        public string Titulo { get; set; } = "";

        [Required(ErrorMessage = "A sinopse é obrigatória.")]
        [DataType(DataType.MultilineText)]
        public string Sinopse { get; set; } = "";

        [Required(ErrorMessage = "A data de lançamento é obrigatória.")]
        [DataType(DataType.Date)]
        [Display(Name = "Data de Lançamento")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateOnly DataLancamento { get; set; }

        [Required(ErrorMessage = "A duração do filme é obrigatória.")]
        [Display(Name = "Duração")]
        [Range(1, 600, ErrorMessage = "Duração inválida (em minutos).")]
        public int Duracao { get; set; }

        [Required(ErrorMessage = "A avaliação é obrigatória.")]
        [Range(1, 5, ErrorMessage = "A avaliação deve estar entre 1 e 5.")]
        [Display(Name = "Avaliação")]
        public int Avaliacao { get; set; } = 3; // default
    }
}