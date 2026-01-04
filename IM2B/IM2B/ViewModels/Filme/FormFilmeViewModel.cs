using System;

namespace IM2B.ViewModels.Filme
{
    public class FormFilmeViewModel
    {
        public int Id { get; set; } // needed for Edit
        public string Titulo { get; set; } = "";
        public string Sinopse { get; set; } = "";
        public DateOnly DataLancamento { get; set; }
        public TimeSpan Duracao { get; set; }
        public int Avaliacao { get; set; } = 3; // default value
    }
}
